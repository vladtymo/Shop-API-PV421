using BusinessLogic.DTOs;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Interfaces;
using BusinessLogic;

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
        public IActionResult GetAll(int? filterCategoryId, string? searchTitle)
        {
            // ?
            return Ok(productsService.GetAll(filterCategoryId, searchTitle));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(productsService.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            var result = productsService.Create(model);

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

            productsService.Edit(model);

            return Ok(); // 200
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            productsService.Delete(id);

            return NoContent(); // 204
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage);
        }
    }
}
