using PM.DAL.Repositories;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Utilities.Filter;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PM.Web.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : BaseApiController
    {
        private readonly ITaskFacade _taskFacade;
        public TaskController(ITaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }

        public TaskController()
        {
            _taskFacade = new TaskFacade(new TaskRepository());
        }

        [Route("search")]
        [HttpPost()]
        [ResponseType(typeof(List<UserDto>))]
        public IHttpActionResult Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Search(filterState));
            });
        }

        [Route("getTasks")]
        [ResponseType(typeof(List<TaskDto>))]
        [HttpGet]
        // GET: api/task/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }
        [Route("{id}")]
        [ResponseType(typeof(TaskDto))]
        [HttpGet]
        // GET: api/task/1
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(TaskDto))]
        [HttpPost]
        // POST: api/task/update
        public IHttpActionResult Update(TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/task/delete/1
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Delete(id));
            });
        }

        [Route("updateTaskStatus")]
        [ResponseType(typeof(bool))]
        [HttpPost()]
        // POST: api/task/updateTaskStatus
        public IHttpActionResult UpdateTaskStatus([FromBody] TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.UpdateTaskStatus(task.Id, task.StatusId));
            });
        }
    }
}