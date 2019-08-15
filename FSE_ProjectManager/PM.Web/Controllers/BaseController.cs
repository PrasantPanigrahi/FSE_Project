using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Extensions.DTO;

namespace PM.Web.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        // Declaration
        public delegate ActionResult WebDelegateMethod();

        protected virtual ActionResult Try(WebDelegateMethod method)
        {
            try
            {
                return method();
            }
            catch (Exception exception)
            {
                //Log.Fatal(exception.Message, exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
