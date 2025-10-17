using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop_Api_PV421.Helpers;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int? filterCategoryId, string? searchTitle)
        {
            return Ok(await productsService.GetAll(filterCategoryId, searchTitle));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await productsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            var result = await productsService.Create(model);

            // 201
            return CreatedAtAction(
                nameof(Get),            // The action to get a single product
                new { id = result.Id }, // Route values for that action
                result                  // Response body
            );
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProductDto model)
        {
            // model validation
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            await productsService.Edit(model);

            return Ok(); // 200
        }

        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);

            return NoContent(); // 204
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage);
        }
    }
}
