using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Utilities.Filter
{
    public static class FilterStateHelper
    {
        #region Extension Methods

        public static string ToOperatorStr(this FilterOperator value)
        {
            switch (value)
            {
                case FilterOperator.Contains: return "contains";

                case FilterOperator.LessThanEqual: return "lte";

                case FilterOperator.GreaterThanEqual: return "gte";

                case FilterOperator.EqualTo: return "eq";

                default:
                    throw new NotImplementedException($"[{value.ToString()}] Operator not handled");
            }
        }

        public static void AddFilter(this FilterState filterState, string fieldName, object value, FilterOperator fieldOperator)
        {
            if (filterState.Filter == null)
            {
                filterState.Filter = new CompositeFilterDescriptor()
                {
                    Logic = "and",
                    Filters = new List<dynamic>()
                };
            }

            var found = SearchFilterDescriptors(filterState.Filter, fieldName).FirstOrDefault();
            if (found != null)
            {
                throw new InvalidOperationException($"filter [${fieldName}] already exists");
            }

            var filterJson = new FilterDescriptor()
            {
                Field = fieldName,
                Value = value,
                Operator = fieldOperator.ToOperatorStr(),
            }.ToJson();

            filterState.Filter.Filters.Add(filterJson);
        }

        #endregion Extension Methods

        public static IEnumerable<FilterDescriptor> SearchFilterDescriptors(CompositeFilterDescriptor root, string fieldName)
        {
            if (root == null) return new List<FilterDescriptor>();

            return FlattenCompositeFilterDescriptor(root)
                .Where(x => x.Field.Trim().ToLower() == fieldName.Trim().ToLower());
        }

        /// <summary>
        /// Flatten out a CompositeFilterDescriptor to FilterDescriptor[]
        /// </summary>
        public static IEnumerable<FilterDescriptor> FlattenCompositeFilterDescriptor(CompositeFilterDescriptor root)
        {
            var result = new List<FilterDescriptor>();

            if (root?.Filters != null)
            {
                // current system only support 'and'
                if (root.Logic.ToLower() == "and")
                {
                    foreach (var filterCriteria in root.Filters)
                    {
                        string json = filterCriteria.ToString();

                        if (json.IndexOf("logic") > -1)
                        {
                            var filterDescriptor = json.ToObject<CompositeFilterDescriptor>();
                            // recursive loop
                            result.AddRange(FlattenCompositeFilterDescriptor(filterDescriptor));
                        }
                        else
                        {
                            var filterDescriptor = json.ToObject<FilterDescriptor>();
                            result.Add(filterDescriptor);
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException("Logic not handled");
                }
            }

            return result;
        }
    }
}
