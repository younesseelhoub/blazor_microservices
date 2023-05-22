using Microsoft.AspNetCore.Mvc;
using ProductOwner.MIcroservices.Model;
using ProductOwner.MIcroservices.Services;

namespace ProductOwner.MIcroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> ProductListAsync()
        {
            var productList = productService.GetProductListAsyncc();
            return productList;

        }

        [HttpGet("{id}")]
        public Task<ProductDetails> GetProductDetailsAsync(int Id)
        {

            return productService.GetProductByIdAsync(Id);

        }

        [HttpPost]
        public Task<Product> AddProductAsync(Product productDetails)
        {
            var productData = productService.AddProductAsyncc(productDetails);
            return productData;
        }

        [HttpPost("sendoffer")]
        public bool SendProductOfferAsync(ProductOfferDetail productOfferDetail)
        {
            bool isSent = false;
            if (productOfferDetail != null)
            {
                isSent = productService.SendProductOffer(productOfferDetail);
            }

            return isSent;


        }

    }
}
