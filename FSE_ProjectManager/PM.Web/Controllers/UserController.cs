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
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserFacade _userFacade;
        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public UserController()
        {
            _userFacade = new UserFacade(new PM.DAL.Repositories.UserRepository());
        }

        [Route("search")]
        [HttpPost()]
        [ResponseType(typeof(List<UserDto>))]
        public IHttpActionResult Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Search(filterState));
            });
        }

        [Route("getUserList")]
        [ResponseType(typeof(List<UserDto>))]
        [HttpGet]
        // GET: api/users/getUserList
        public IHttpActionResult GetUsers()
        {
            return Try(() =>
            {
                return Ok(_userFacade.GetAll());
            });
        }

        
        [Route("{id}")]
        [ResponseType(typeof(UserDto))]
        [HttpGet]
        // GET: api/user/1
        public IHttpActionResult GetUser(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(UserDto))]
        [HttpPost]
        // POST: api/user/update
        public IHttpActionResult Update(UserDto user)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Update(user));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/user/1
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Delete(id));
            });
        }
    }
}