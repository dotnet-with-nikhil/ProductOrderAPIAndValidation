using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Helpers.Responses;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_example_clean_arch_with_entity_framework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Quantity = o.Quantity,
                ProductId = o.ProductId,
                ProductName = o.ProductName
            });

            return Ok(ResponseHelper.Success(result, "Orders fetched successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);

            if (order == null)
                return NotFound(ResponseHelper.Fail<string>("Order not found"));

            var result = new OrderDto
            {
                Id = order.Id,
                Quantity = order.Quantity,
                ProductId = order.ProductId,
                ProductName = order.Product?.Name
            };

            return Ok(ResponseHelper.Success(result, "Order fetched successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var product = await _productService.GetById(dto.ProductId);

            if (product == null)
                return BadRequest(ResponseHelper.Fail<string>("Invalid ProductId"));

            var order = new Order
            {
                Quantity = dto.Quantity,
                ProductId = dto.ProductId
            };

            var created = await _orderService.Add(order);

            var result = new OrderDto
            {
                Id = created.Id,
                Quantity = created.Quantity,
                ProductId = created.ProductId,
                ProductName = product.Name
            };

            return Ok(ResponseHelper.Success(result, "Order created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
        {
            var order = await _orderService.GetById(id);

            if (order == null)
                return NotFound(ResponseHelper.Fail<string>("Order not found"));

            order.Quantity = dto.Quantity;

            await _orderService.Update(id, order);

            return Ok(ResponseHelper.Success<string>(null, "Order updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetById(id);

            if (order == null)
                return NotFound(ResponseHelper.Fail<string>("Order not found"));

            await _orderService.Update(id, order);

            return Ok(ResponseHelper.Success<string>(null, "Order deleted successfully"));
        }
    }
}
