using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext ctx;

        public ProductsController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var items = ctx.Products.ToList();

            return Ok(items);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id < 0) 
                return BadRequest("Id can not be negative!");

            var item = ctx.Products.Find(id);

            if (item == null) 
                return NotFound("Product not found!");

            return Ok(item);
        }

        //public IActionResult Create() 
        //{

        //}

        //public IActionResult Edit()
        //{

        //}

        //public IActionResult Delete(int id)
        //{

        //}
    }
}
