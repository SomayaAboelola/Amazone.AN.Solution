using Amazone.Apis.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Errors(int code)
        {
            if (code == 400)
                return BadRequest(new ResponseApi(code));
            else if (code == 401)
                return Unauthorized(new ResponseApi(code));
            else if (code == 404)
                return NotFound(new ResponseApi(code));
            return StatusCode(code);
        }
    }
}
