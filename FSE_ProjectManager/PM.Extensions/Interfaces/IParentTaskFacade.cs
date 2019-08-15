using PM.Extensions.DTO;
using System.Collections.Generic;

namespace PM.Extensions.Interfaces
{
    public interface IParentTaskFacade
    {
        ParentTaskDto Get(int Id);

        List<ParentTaskDto> GetAll();

        ParentTaskDto Update(ParentTaskDto task);
    }
}