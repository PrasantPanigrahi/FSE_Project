using Microsoft.AspNetCore.Mvc;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Utilities.Filter;
using System.Collections.Generic;
using System.Net;

namespace PM.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [Route("search")]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<FilterResult<UserDto>> Search([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Search(filterState));
            });
        }

        [Route("getUserList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/users/getUserList
        public ActionResult<List<UserDto>> GetUserList()
        {
            return Try(() =>
            {
                return Ok(_userFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        // GET: api/user/1
        public ActionResult<UserDto> GetUser(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Get(id));
            });
        }

        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        // POST: api/user/update
        public ActionResult<UserDto> Update(UserDto user)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Update(user));
            });
        }

        [Route("delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete]
        // DELETE: api/user/5
        public ActionResult<bool> Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Delete(id));
            });
        }
    }
}