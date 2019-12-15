using System;
using System.Collections.Generic;

namespace SearchEngine.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Engine { get; set; }
        public string SearchWord { get; set; }
        public int ResultId { get; set; }
        public virtual IList<Result> Results { get; set; }
    }
}
