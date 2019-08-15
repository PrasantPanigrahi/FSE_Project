using PM.Extensions.DTO;
using PM.Utilities.Filter;
using System.Collections.Generic;

namespace PM.Extensions.Interfaces
{
    public interface ITaskFacade
    {
        TaskDto Get(int Id);

        List<TaskDto> GetAll();

        TaskDto Update(TaskDto task);

        bool Delete(int id);

        FilterResult<TaskDto> Search(FilterState filterState);

        bool UpdateTaskStatus(int taskId, int statusId);
    }
}