using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;

namespace dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<bool> Exists(int id);
    }
}
