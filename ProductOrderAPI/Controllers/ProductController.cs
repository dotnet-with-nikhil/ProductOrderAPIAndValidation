using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Helpers.Responses;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_example_clean_arch_with_entity_framework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _iProductService;
        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _iProductService.GetAll();
            return Ok(ResponseHelper.Success(products, "Products fetched successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _iProductService.GetById(id);

            if (product == null)
                return NotFound(ResponseHelper.Fail<string>("Product not found"));

            return Ok(ResponseHelper.Success(product, "Product fetched"));
        }
            
        [HttpPost]
        public async Task<IActionResult> Post(ProductDto dto)
        {
            var product = new Products
            {
                Name = dto.Name,
                Price = dto.Price
            };

            var id = await _iProductService.Add(product);

            return CreatedAtAction(
                nameof(Get),
                new { id = id },
                ResponseHelper.Success(id, "Product created successfully")
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto dto)
        {
            var productModelUpdate = new Products
            {
                Name = dto.Name,
                Price = dto.Price
            };

            var updated = await _iProductService.Update(id, productModelUpdate);

            if (!updated)
                return NotFound(ResponseHelper.Fail<string>("Product not found"));

            return Ok(ResponseHelper.Success<string>(null, "Product updated successfully"));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, UpdateProductDto dto)
        {
            var updated = await _iProductService.Patch(id, dto);

            if (!updated)
                return NotFound(ResponseHelper.Fail<string>("Product not found"));

            return Ok(ResponseHelper.Success<string>(null, "Product partially updated"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _iProductService.Detele(id);

            if (!deleted)
                return NotFound(ResponseHelper.Fail<string>("Product not found"));

            return Ok(ResponseHelper.Success<string>(null, "Product deleted successfully"));
        }


        [HttpHead("{id}")]
        public async Task<IActionResult> Head(int id)
        {
            var exists = await _iProductService.IsExists(id);
            if (!exists)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Resource-Exists", "true");
            return Ok();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, HEAD, OPTIONS");
            return Ok();
        }


    }
}
