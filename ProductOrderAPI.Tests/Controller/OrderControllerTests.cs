using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using dotnet_example_clean_arch_with_entity_framework.Controllers;
using dotnet_example_clean_arch_with_entity_framework.Helpers.Responses;
using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;

public class OrderControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<IProductService> _productServiceMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _productServiceMock = new Mock<IProductService>();

        _controller = new OrderController(
            _orderServiceMock.Object,
            _productServiceMock.Object
        );
    }

    [Fact]
    public async Task GetAll_Should_Return_Ok_With_Orders()
    {
        // Arrange
        var orders = new List<OrderDto>
        {
            new OrderDto { Id = 1, Quantity = 2, ProductId = 10, ProductName = "Laptop" }
        };

        _orderServiceMock.Setup(x => x.GetAll()).ReturnsAsync(orders);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();

        var response = okResult.Value as ApiResponse<IEnumerable<OrderDto>>;
        response.Success.Should().BeTrue();
        response.Data.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_Order_Not_Exists()
    {
        _orderServiceMock.Setup(x => x.GetById(It.IsAny<int>()))
                      .ReturnsAsync((Order)null);

        var result = await _controller.GetById(1);

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Create_Should_Return_BadRequest_When_Product_Not_Found()
    {
        _productServiceMock.Setup(x => x.GetById(It.IsAny<int>()))
                        .ReturnsAsync((Products)null);

        var dto = new CreateOrderDto { ProductId = 1, Quantity = 2 };

        var result = await _controller.Create(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Create_Should_Create_Order_When_Valid()
    {
        var product = new Products { Id = 1, Name = "Laptop", Price = 500 };

        _productServiceMock.Setup(x => x.GetById(It.IsAny<int>()))
                        .ReturnsAsync(product);

        _orderServiceMock.Setup(x => x.Add(It.IsAny<Order>()))
                      .ReturnsAsync(new Order { Id = 1, ProductId = 1, Quantity = 2 });

        var dto = new CreateOrderDto { ProductId = 1, Quantity = 2 };

        var result = await _controller.Create(dto);

        result.Should().BeOfType<OkObjectResult>();
    }
}