using PM.Models;
using PM.Utilities.Filter;
using System.Linq;

namespace PM.DAL.Sort
{
    internal class ProjectSort
    {
        public void Sort(SortDescriptor sort, ref IQueryable<Project> query)
        {
            if (sort != null && !string.IsNullOrWhiteSpace(sort.Field) && !string.IsNullOrWhiteSpace(sort.Dir))
            {
                //base.Sort(sort, ref query);

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

                    case "managerdisplayname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Manager.FirstName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Manager.FirstName);
                        }
                        break;

                    case "totaltasks":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.Tasks.Count);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.Tasks.Count);
                        }
                        break;

                    case "issuspendedtext":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.IsSuspended);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.IsSuspended);
                        }
                        break;
                }
            }
        }
    }
}