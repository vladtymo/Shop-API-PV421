using BusinessLogic.DTOs;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsController(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var items = ctx.Products
                .Include(x => x.Category) // perform LEF JOIN
                .ToList();

            return Ok(mapper.Map<IEnumerable<ProductDto>>(items));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id < 0) 
                return BadRequest("Id can not be negative!"); // 400

            var item = ctx.Products.Find(id);

            if (item == null) 
                return NotFound("Product not found!"); // 404

            return Ok(mapper.Map<ProductDto>(item)); // 200
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto model)
        {
            // TODO: reference (class) vs value (structures)

            // model validation
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            var entity = mapper.Map<Product>(model);

            // logic...
            ctx.Products.Add(entity);
            ctx.SaveChanges(); // generate id (execute INSERT SQL command)

            var result = mapper.Map<ProductDto>(entity);

            // 201
            return CreatedAtAction(
                nameof(Get),            // The action to get a single product
                new { id = result.Id }, // Route values for that action
                result                  // Response body
            );
        }

        [HttpPut]
        public IActionResult Edit(EditProductDto model)
        {
            // model validation
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            // logic...
            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();

            return Ok(); // 200
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest("Id can not be negative!");

            var item = ctx.Products.Find(id);

            if (item == null)
                return NotFound("Product not found!");

            ctx.Products.Remove(item);
            ctx.SaveChanges(true);

            return NoContent(); // 204
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage);
        }
    }
}
