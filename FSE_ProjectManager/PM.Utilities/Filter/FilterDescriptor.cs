using System;

namespace PM.Utilities.Filter
{
    public enum FilterOperator
    {
        Undefined = 0,

        /// <summary>
        /// contains
        /// </summary>
        Contains = 1,

        /// <summary>
        /// lt
        /// </summary>
        LessThan = 2,

        /// <summary>
        /// lte
        /// </summary>
        LessThanEqual = 3,

        /// <summary>
        /// gt
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// gte
        /// </summary>
        GreaterThanEqual = 5,

        /// <summary>
        /// eq
        /// </summary>
        EqualTo = 6,

        /// <summary>
        /// neq
        /// </summary>
        NotEqualTo = 7,

        /// <summary>
        /// rng
        /// </summary>
        Range = 8
    }

    public class FilterDescriptor
    {
        /// <summary>
        /// The data item field to which the filter operator is applied.
        /// </summary>
        public string Field { get; set; }

        /**
         * The filter operator (comparison).
         *
         * The supported operators are:
         *
         * * `"eq"` (equal to)
         * * `"neq"` (not equal to)
         * * `"isnull"` (is equal to null)
         * * `"isnotnull"` (is not equal to null)
         * * `"lt"` (less than)
         * * `"lte"` (less than or equal to)
         * * `"gt"` (greater than)
         * * `"gte"` (greater than or equal to)
         * * `"rng"` (range between)
         *
         * The following operators are supported for string fields only:
         *
         * * `"startswith"`
         * * `"endswith"`
         * * `"contains"`
         * * `"doesnotcontain"`
         * * `"isempty"`
         * * `"isnotempty"`
         */
        public string Operator { get; set; }

        public FilterOperator FilterOperator
        {
            get
            {
                switch (Operator.Trim().ToLower())
                {
                    case "contains":
                        return FilterOperator.Contains;

                    case "lte":
                        return FilterOperator.LessThanEqual;

                    case "gte":
                        return FilterOperator.GreaterThanEqual;

                    case "eq":
                        return FilterOperator.EqualTo;

                    case "neq":
                        return FilterOperator.NotEqualTo;

                    default:
                        throw new NotImplementedException($"[{Operator.Trim().ToLower()}] Operator not handled");
                }
            }
        }

        /// <summary>
        /// The value to which the field is compared. Has to be of the same type as the field.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Determines if the string comparison is case-insensitive.
        /// </summary>
        public bool IgnoreCase { get; set; }
    }
}