using Xunit;
using Moq;
using FluentAssertions;
using dotnet_example_clean_arch_with_entity_framework.Services;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Models;
using System.Threading.Tasks;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _repoMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _service = new OrderService(_repoMock.Object);
    }

    [Fact]
    public async Task Update_Should_Return_False_When_Order_Not_Exists()
    {
        _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync((Order)null);

        var result = await _service.Update(1, new Order { Quantity = 2 });

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Update_Should_Update_Order_When_Exists()
    {
        var order = new Order { Id = 1, Quantity = 1 };

        _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(order);

        var result = await _service.Update(1, new Order { Quantity = 5 });

        result.Should().BeTrue();
        order.Quantity.Should().Be(5);
    }

    [Fact]
    public async Task Delete_Should_Return_False_When_Not_Exists()
    {
        _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync((Order)null);

        var result = await _service.Detele(1);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_Should_Return_True_When_Deleted()
    {
        var order = new Order { Id = 1 };

        _repoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(order);

        var result = await _service.Detele(1);

        result.Should().BeTrue();
    }
}