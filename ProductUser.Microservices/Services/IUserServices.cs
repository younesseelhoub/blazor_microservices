using ProductUser.Microservices.Model;

namespace ProductUser.Microservices.Services
{
    public interface IUserServices
    {
        public Task<IEnumerable<OrderItem>> GetProductListAsync();
        public Task<OrderItem> GetProductByIdAsync(int id);
        public Task<OrderItem> AddProductAsync(OrderItem product);
        bool SendProductOffer(Product productOfferDetails);

    }
}
