using PM.Extensions.DTO;
using PM.Utilities.Filter;
using System.Collections.Generic;

namespace PM.Extensions.Interfaces
{
    public interface IProjectFacade
    {
        ProjectDto Get(int Id);

        List<ProjectDto> GetAll();

        ProjectDto Update(ProjectDto user);

        bool Delete(int id);

        FilterResult<ProjectDto> Search(FilterState filterState);

        bool UpdateProjectStatus(int projectId, bool isSuspended);
    }
}