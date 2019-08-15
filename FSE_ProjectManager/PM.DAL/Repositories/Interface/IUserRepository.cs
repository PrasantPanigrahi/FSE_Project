using PM.Models;
using PM.Utilities.Filter;

namespace PM.DAL.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        FilterResult<User> Search(FilterState filterState);
    }
}