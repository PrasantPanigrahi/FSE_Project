using System.Collections.Generic;

namespace PM.Utilities.Filter
{
    public class FilterState
    {
        /// <summary>
        /// The number of records to be skipped by the pager.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The number of records to take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// The descriptors used for sorting.
        /// </summary>
        public List<SortDescriptor> Sort { get; set; }

        /// <summary>
        /// The descriptors used for filtering.
        /// </summary>
        public CompositeFilterDescriptor Filter { get; set; }

        /// <summary>
        /// Time zone offset (minutes) with client browser.
        /// Only applicable to DateTime
        /// </summary>
        public double? TimeZoneOffset { get; set; }
    }
}