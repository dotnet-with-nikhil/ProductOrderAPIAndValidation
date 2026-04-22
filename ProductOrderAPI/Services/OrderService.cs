using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;

namespace dotnet_example_clean_arch_with_entity_framework.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository orderRepository)
        {
            _repo = orderRepository;
        }


        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Order> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Order> Add(Order order)
        {
            return await _repo.CreateAsync(order);
        }

        public async Task<bool> Update(int id, Order order)
        {
            var orderModel = await _repo.GetByIdAsync(id);

            if (orderModel == null)
                throw new Exception("Order not found");

            orderModel.Quantity = order.Quantity;
            orderModel.ProductId = order.ProductId;
            orderModel.Id = order.Id;
            orderModel.Product = order.Product;
            await _repo.UpdateAsync(orderModel);
            return true;
        }

        public async Task<bool> Patch(int id, UpdateOrderDto dto)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
                return false;

            if (dto.Quantity > 0)
                order.Quantity = dto.Quantity;

            await _repo.UpdateAsync(order);
            return true;
        }

        public async Task<bool> Detele(int id)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
                return false;

            await _repo.DeleteAsync(order);
            return true;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _repo.Exists(id);
        }

    }
}
