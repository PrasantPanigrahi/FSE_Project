using PM.Extensions.DTO;
using PM.Utilities.Filter;
using System.Collections.Generic;

namespace PM.Extensions.Interfaces
{
    public interface IUserFacade
    {
        UserDto Get(int Id);

        List<UserDto> GetAll();

        UserDto Update(UserDto user);

        bool Delete(int id);

        FilterResult<UserDto> Search(FilterState filterState);
    }
}