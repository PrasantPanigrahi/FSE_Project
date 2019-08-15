using PM.Utilities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.DAL.Filters
{
    internal class TaskFilter
    {
        public void CompositeFilter(CompositeFilterDescriptor root, ref IQueryable<Models.Task> query)
        {
            var filters = FilterStateHelper.FlattenCompositeFilterDescriptor(root);

            foreach (var f in filters)
                Filter(f, ref query);
        }

        public void Filter(FilterDescriptor filter, ref IQueryable<Models.Task> query)
        {
            if (filter != null && !string.IsNullOrWhiteSpace(filter.Field) && !string.IsNullOrWhiteSpace(filter.Operator))
            {
                switch (filter.Field.Trim().ToLower())
                {
                    case "id":
                        if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Id.ToString().ToLower() == filter.Value.ToString().ToLower());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "name":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.Name.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Name == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "priority":
                        int.TryParse(filter.Value.ToString(), out int filterValue);
                        if (filter.FilterOperator == FilterOperator.GreaterThanEqual)
                        {
                            query = query.Where(q => q.Priority >= filterValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.LessThanEqual)
                        {
                            query = query.Where(q => q.Priority <= filterValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Priority == filterValue);
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "parenttaskname":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.ParentTask.Name.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.ParentTask.Name == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "ownername":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.Owner.FirstName.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Owner.FirstName == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                    case "projectname":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.Project.Name.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Project.Name == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "status":
                        if (filter.FilterOperator == FilterOperator.NotEqualTo)
                        {
                            filterValue = Convert.ToInt32(filter.Value.ToString());
                            query = query.Where(q => q.StatusId != filterValue);
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                }
            }
        }
    }
}
