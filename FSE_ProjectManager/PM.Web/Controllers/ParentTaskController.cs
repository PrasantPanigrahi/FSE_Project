using Microsoft.AspNetCore.Mvc;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace PM.Web.Controllers
{
    [Route("api/parentTask")]
    [ApiController]
    public class ParentTaskController : BaseController
    {
        private readonly IParentTaskFacade _taskFacade;

        public ParentTaskController(IParentTaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }             

        [Route("getTasks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/parentTask/getTasks
        public ActionResult<List<ParentTaskDto>> GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/parentTask/1
        public ActionResult<ParentTaskDto> GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        // POST: api/parentTask/update
        public ActionResult<ParentTaskDto> Update(ParentTaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }
    }
}