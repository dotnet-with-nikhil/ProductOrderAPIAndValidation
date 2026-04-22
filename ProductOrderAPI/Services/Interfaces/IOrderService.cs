using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;

namespace dotnet_example_clean_arch_with_entity_framework.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAll();
        Task<Order> GetById(int id);
        Task<Order> Add(Order product);
        Task<bool> Update(int id, Order dto);
        Task<bool> Patch(int id, UpdateOrderDto product);
        Task<bool> Detele(int id);
        Task<bool> IsExists(int id);
    }
}
