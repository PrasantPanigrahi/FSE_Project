using PM.Utilities.Filter;

namespace PM.DAL.Repositories.Interface
{
    public interface ITaskRepository : IRepository<Models.Task>
    {
        FilterResult<Models.Task> Search(FilterState filterState);
    }
}