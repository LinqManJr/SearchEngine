using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Helpers
{
    public static class TaskExtensions
    {
        public static List<Task<T>> OrderByCompletion<T>(this IEnumerable<Task<T>> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var taskArray = source.ToArray();

            var numTasks = taskArray.Length;
            var tcs = new TaskCompletionSource<T>[numTasks];
            var ret = new List<Task<T>>(numTasks);

            var lastIndex = -1;

            Action<Task<T>> continuation = task =>
            {
                var index = Interlocked.Increment(ref lastIndex);
                tcs[index].TryCompleteFromCompletedTask(task);
            };

            for (var i = 0; i != numTasks; ++i)
            {
                tcs[i] = new TaskCompletionSource<T>();
                ret.Add(tcs[i].Task);
                taskArray[i].ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously
                                | TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);
            }

            return ret;
        }
        private static bool TryCompleteFromCompletedTask<TResult, TSourceResult>(this TaskCompletionSource<TResult> completionSource, Task<TSourceResult> task) where TSourceResult : TResult
        {
            if (completionSource == null)
                throw new ArgumentNullException(nameof(completionSource));

            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.IsFaulted)
                return completionSource.TrySetException(task.Exception.InnerExceptions);

            if (task.IsCanceled)
            {
                try
                {
                    task.WaitAndUnwrapException();
                }
                catch (OperationCanceledException exception)
                {
                    var token = exception.CancellationToken;
                    return token.IsCancellationRequested ? completionSource.TrySetCanceled(token) : completionSource.TrySetCanceled();
                }
            }
            return completionSource.TrySetResult(task.Result);
        }
        private static void WaitAndUnwrapException(this Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            task.GetAwaiter().GetResult();
        }
    }
}
