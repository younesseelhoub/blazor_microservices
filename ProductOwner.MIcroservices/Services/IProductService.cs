using ProductOwner.MIcroservices.Model;

namespace ProductOwner.MIcroservices.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetProductListAsync();



        Task<ProductDetails> GetProductByIdAsync(int id);

        Task<ProductDetails> AddProductAsync(ProductDetails productDetails);

        Task<Product> AddProductAsyncc(Product productDetails);


        Task<IEnumerable<Product>> GetProductListAsyncc();


        bool SendProductOffer(ProductOfferDetail productOfferDetails);
    }
}
