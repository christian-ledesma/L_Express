using Microsoft.AspNetCore.Mvc;

namespace LExpress.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this is a list of products";
        }

        [HttpGet]
        [Route("{id}")]
        public string GetProduct(int id)
        {
            return "this is a single products";
        }
    }
}
