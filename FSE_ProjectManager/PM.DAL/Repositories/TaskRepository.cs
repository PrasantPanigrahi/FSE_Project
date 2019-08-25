using PM.DAL.Filters;
using PM.DAL.Repositories.Interface;
using PM.DAL.Sort;
using PM.Models;
using PM.Utilities.Filter;
using System;
using System.Linq;
using System.Data.Entity;

namespace PM.DAL.Repositories
{
    public class TaskRepository : Repository<Models.Task>, ITaskRepository
    {
        public FilterResult<Models.Task> Search(FilterState filterState)
        {
            var result = new FilterResult<Models.Task>();
            IQueryable<Models.Task> query = Context.Tasks.Include(t => t.ParentTask);

            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new TaskFilter();
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
                        var purchaseOrderSort = new TaskSort();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    result.Data = query
                                 .Skip(filterState.Skip)
                                 .Take(filterState.Take)
                                 .ToList();
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

        public override Task Get(int id)
        {
            return Context.Tasks.Include(t => t.ParentTask)
                                                    .Include(t => t.Owner)
                                                    .Include(t => t.Project)
                                                    .Where(t => t.Id == id)
                                                    .DefaultIfEmpty()
                                                    .First();
        }
    }
}