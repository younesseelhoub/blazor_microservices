using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using System.Net;

namespace clientBLAZOR.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> CreateCatalog(ProductDto catalog)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Catalog", catalog);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<ProductDto> UpdateCatalog(ProductDto catalog)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Catalog/{catalog.Id}", catalog);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<ProductDto> GetCatalog(int id)
        {
            var response = await _httpClient.GetAsync($"api/catalog/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetCatalogs()
        {
            try
            {
                var response = await _httpClient.GetAsync("gateway/product");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
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

        public async Task<bool> DeleteCatalog(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Catalog/{id}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
