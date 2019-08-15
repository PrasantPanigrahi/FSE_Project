using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Utilities.Filter
{
    public class FilterResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int? Total { get; set; }
    }
}
