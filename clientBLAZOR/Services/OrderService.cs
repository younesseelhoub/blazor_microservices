using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using System.Net;

namespace clientBLAZOR.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }
        public async Task<OrderDto> CreateOrder(OrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("gateway/offers", order);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(OrderDto);
                }

                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Order/{id}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var response = await _httpClient.GetAsync($"api/Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(OrderDto);
                }

                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            try
            {
                var response = await _httpClient.GetAsync("gateway/offers");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<OrderDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDto> UpdateOrder(OrderDto order)
        {
            var response = await _httpClient.PutAsJsonAsync("gateway/offers", order);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(OrderDto);
                }

                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
    }
}
