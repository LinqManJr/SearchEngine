using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Engine { get; set; }
        public int ResultId { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
