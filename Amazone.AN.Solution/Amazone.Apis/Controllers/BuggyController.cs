using Amazone.Apis.Errors;
using Amazone.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Amazone.Apis.Controllers
{

    public class BuggyController : BaseController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var product = _context.Products.Find(100);
            if (product is null)
                return NotFound(new ResponseApi(404));

            return Ok(product);
        }
        [HttpGet("nullreference")]
        public ActionResult GetNullReference()
        {
            var product = _context.Products.Find(100);
            var ReturnProduct = product.ToString();

            return Ok(new ResponseApi(400));
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ResponseApi(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok(new ResponseApi(400));

        }
        [HttpGet("unauthorize")]
        public ActionResult GetUnauthorize()
        {
            return Unauthorized(new ResponseApi(401));
        }

    }
}
