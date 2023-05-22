using Microsoft.AspNetCore.Mvc;
using ProductUser.Microservices.Model;
using ProductUser.Microservices.Services;

namespace ProductUser.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOffersController : ControllerBase
    {
        private readonly IUserServices userService;

        public UserOffersController(IUserServices _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public Task<IEnumerable<OrderItem>> ProductListAsync()
        {
            var productList = userService.GetProductListAsync();
            return productList;

        }

        [HttpGet("{id}")]
        public Task<OrderItem> GetProductByIdAsync(int Id)
        {
            return userService.GetProductByIdAsync(Id);
        }

        [HttpPost]
        public Task<OrderItem> AddProductAsync(OrderItem productDetails)
        {
            var productData = userService.AddProductAsync(productDetails);
            var product = new Product
            {
                Id = 1,
                Name = "one",
                Description = "one",
                Price = 10,
                StockQuantity = 1
            };
            userService.SendProductOffer(product);
            return productData;
        }

    }
}
