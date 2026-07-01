using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop_Api_PV421.Helpers;

namespace Shop_Api_PV421.Controllers
{
    /// <summary>
    /// Manages product CRUD operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        /// <summary>
        /// Returns all products with optional category and title filters.
        /// </summary>
        /// <param name="filterCategoryId">Optional category id used to filter products.</param>
        /// <param name="searchTitle">Optional case-insensitive product title search term.</param>
        /// <returns>A filtered list of products.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int? filterCategoryId, string? searchTitle)
        {
            return Ok(await productsService.GetAll(filterCategoryId, searchTitle));
        }

        /// <summary>
        /// Returns a single product by id.
        /// </summary>
        /// <param name="id">The unique product identifier.</param>
        /// <returns>The requested product.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await productsService.Get(id));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="model">The payload required to create a product.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="model">The payload containing product update values.</param>
        /// <returns>No content other than the HTTP status code.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(EditProductDto model)
        {
            // model validation
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            await productsService.Edit(model);

            return Ok(); // 200
        }

        /// <summary>
        /// Deletes a product by id.
        /// </summary>
        /// <param name="id">The unique product identifier.</param>
        /// <returns>No content when the delete succeeds.</returns>
        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
