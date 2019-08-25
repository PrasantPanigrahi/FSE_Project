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
    [RoutePrefix("api/project")]
    public class ProjectController : BaseApiController
    {
        private readonly IProjectFacade _projectFacade;
        public ProjectController(IProjectFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }
        public ProjectController()
        {
            _projectFacade = new ProjectFacade(new ProjectRepository());
        }

        [Route("search")]
        [HttpPost()]
        [ResponseType(typeof(List<ProjectDto>))]
        public IHttpActionResult Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Search(filterState));
            });
        }

        [Route("getProjects")]
        [ResponseType(typeof(List<ProjectDto>))]
        [HttpGet]
        // GET: api/project/getProjects
        public IHttpActionResult GetProjects()
        {
            return Try(() =>
            {
                return Ok(_projectFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ProjectDto))]
        [HttpGet]
        // GET: api/project/5
        public IHttpActionResult GetProject(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Get(id));
            });
        }

        [Route("updateProjectStatus")]
        [ResponseType(typeof(bool))]
        [HttpPost()]
        // POST: api/project/suspend/?id=10
        public IHttpActionResult UpdateProjectStatus(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.UpdateProjectStatus(project.Id, project.IsSuspended));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ProjectDto))]
        [HttpPost]
        // POST: api/project/update
        public IHttpActionResult Update(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Update(project));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/project/5
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Delete(id));
            });
        }
    }
}
