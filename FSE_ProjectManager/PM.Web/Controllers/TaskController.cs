using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Utilities.Filter;
using System;
using System.Collections.Generic;

namespace PM.Web.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : BaseController
    {
        private readonly ITaskFacade _taskFacade;

        public TaskController(ITaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }   

        [Route("search")]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<FilterResult<TaskDto>> Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Search(filterState));
            });
        }

        [Route("getTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        // GET: api/task/getTasks
        public ActionResult<List<TaskDto>> GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        // GET: api/task/1
        public ActionResult<TaskDto> GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        // POST: api/task/update
        public ActionResult<TaskDto> Update(TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }

        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        // DELETE: api/task/delete/1
        public ActionResult<Boolean> Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Delete(id));
            });
        }

        [Route("updateTaskStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost()]
        // POST: api/task/updateTaskStatus
        public ActionResult<bool> UpdateTaskStatus([FromBody] TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.UpdateTaskStatus(task.Id, task.StatusId));
            });
        }
    }
}