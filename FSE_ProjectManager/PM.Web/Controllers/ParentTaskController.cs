using PM.DAL.Repositories;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PM.Web.Controllers
{
    [RoutePrefix("api/parentTask")]
    public class ParentTaskController : BaseApiController
    {
        private readonly IParentTaskFacade _taskFacade;

        public ParentTaskController(IParentTaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }

        public ParentTaskController()
        {
            _taskFacade = new ParentTaskFacade(new ParentTaskRepository());
        }

        [Route("getTasks")]
        [ResponseType(typeof(List<ParentTaskDto>))]
        [HttpGet]
        // GET: api/parentTask/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentTaskDto))]
        [HttpGet]
        // GET: api/parentTask/5
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ParentTaskDto))]
        [HttpPost]
        // POST: api/parentTask/update
        public IHttpActionResult Update(ParentTaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }
    }
}