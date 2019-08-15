using PM.Models;
using PM.Utilities.Filter;
using System.Linq;

namespace PM.DAL.Sort
{
    internal class UserSort
    {
        public void Sort(SortDescriptor sort, ref IQueryable<User> query)
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

                    case "firstname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.FirstName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.FirstName);
                        }
                        break;

                    case "lastname":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.LastName);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.LastName);
                        }
                        break;

                    case "employeeid":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(q => q.EmployeeId);
                        }
                        else
                        {
                            query = query.OrderByDescending(q => q.EmployeeId);
                        }
                        break;
                }
            }
        }
    }
}