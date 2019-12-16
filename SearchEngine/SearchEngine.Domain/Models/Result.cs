﻿using SearchEngine.Core.Models;
using System.Collections.Generic;

namespace SearchEngine.Domain.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int ItemsCount { get; set; }
        public virtual IList<Request> Requests { get; set; }
        public IList<ItemResult> Items { get; set; }

    }
}