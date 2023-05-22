using clientBLAZOR.Dtos;

namespace clientBLAZOR.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrders();
        Task<OrderDto> GetOrder(int id);
        Task<bool> DeleteOrder(int id);
        Task<OrderDto> CreateOrder(OrderDto catalog);
        Task<OrderDto> UpdateOrder(OrderDto catalog);
    }
}
