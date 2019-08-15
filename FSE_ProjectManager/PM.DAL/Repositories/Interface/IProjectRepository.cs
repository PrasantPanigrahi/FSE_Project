using PM.Models;
using PM.Utilities.Filter;

namespace PM.DAL.Repositories.Interface
{
    public interface IProjectRepository : IRepository<Project>
    {
        FilterResult<Project> Search(FilterState filterState);
    }
}