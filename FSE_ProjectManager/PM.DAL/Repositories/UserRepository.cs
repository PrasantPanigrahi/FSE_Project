using PM.DAL.Filters;
using PM.DAL.Repositories.Interface;
using PM.DAL.Sort;
using PM.Models;
using PM.Utilities.Filter;
using System;
using System.Linq;

namespace PM.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public FilterResult<User> Search(FilterState filterState)
        {
            var result = new FilterResult<User>();
            IQueryable<User> query = Context.Users;
            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new UserFilter();
                    if (filterState.Filter.Logic.ToLower() == "and")
                    {
                        filter.CompositeFilter(filterState.Filter, ref query);
                    }
                    else
                    {
                        throw new NotImplementedException("Logic not handled");
                    }
                }

                // Sorting
                if (filterState.Sort != null)
                {
                    foreach (var sort in filterState.Sort)
                    {
                        var purchaseOrderSort = new UserSort();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    var x = query
                                 .Skip(filterState.Skip)
                                 .Take(filterState.Take)
                                 .ToList();
                    result.Data = x;
                }
                else
                {
                    result.Data = query.ToList();
                }
            }
            else
            {
                result.Data = query.ToList();
            }

            // Get total records count
            result.Total = query.Count();

            return result;
        }
    }
}