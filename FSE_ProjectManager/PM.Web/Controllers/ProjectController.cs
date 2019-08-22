using Microsoft.AspNetCore.Mvc;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Utilities.Filter;
using System.Collections.Generic;
using System.Net;

namespace PM.Web.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectFacade _projectFacade;

        public ProjectController(IProjectFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }


        [Route("search")]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<FilterResult<ProjectDto>> Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Search(filterState));
            });
        }

        [Route("getProjects")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/project/getProjects
        public ActionResult<List<ProjectDto>> GetProjects()
        {
            return Try(() =>
            {
                return Ok(_projectFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/project/5
        public ActionResult<ProjectDto> GetProject(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Get(id));
            });
        }

        [Route("updateProjectStatus")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost()]
        // POST: api/project/suspend/?id=1
        public ActionResult<ProjectDto> UpdateProjectState(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.UpdateProjectStatus(project.Id, project.IsSuspended));
            });
        }

        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        // POST: api/project/update
        public ActionResult<ProjectDto> Update(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Update(project));
            });
        }

        [Route("delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete]
        // DELETE: api/project/1
        public ActionResult<bool> Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Delete(id));
            });
        }

    }
}