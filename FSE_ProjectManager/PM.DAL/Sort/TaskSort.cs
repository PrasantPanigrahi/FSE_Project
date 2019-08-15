using PM.Models;
using PM.Utilities.Filter;
using System.Linq;

namespace PM.DAL.Sort
{
    internal class TaskSort
    {
        public void Sort(SortDescriptor sort, ref IQueryable<Task> query)
        {
            if (sort != null && !string.IsNullOrWhiteSpace(sort.Field) && !string.IsNullOrWhiteSpace(sort.Dir))
            {
                switch (sort.Field.Trim().ToLower())
                {
                    case "id":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(p => p.Id);
                        }
                        else
                        {
                            query = query.OrderByDescending(p => p.Id);
                        }

                        break;

                    case "name":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Name);
                        }
                        break;

                    case "startdate":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.StartDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.StartDate);
                        }
                        break;

                    case "enddate":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.EndDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.EndDate);
                        }
                        break;

                    case "parenttaskname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.ParentTask.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.ParentTask.Name);
                        }
                        break;

                    case "priority":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Priority);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Priority);
                        }
                        break;

                    case "ownername":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Owner.FirstName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Owner.FirstName);
                        }
                        break;

                    case "projectname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Project.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Project.Name);
                        }
                        break;
                }
            }
        }
    }
}