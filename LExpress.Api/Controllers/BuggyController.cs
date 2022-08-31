using Infrastructure.Data;
using LExpress.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LExpress.Api.Controllers
{
    public class BuggyController : BaseController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var response = _context.Products.Find(1000);
            if (response == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet]
        [Route("servererror")]
        public ActionResult GetServerErrir()
        {

            var response = _context.Products.Find(1000);
            response.ToString();
            return Ok();
        }

        [HttpGet]
        [Route("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet]
        [Route("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
