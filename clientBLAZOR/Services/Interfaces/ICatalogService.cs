using clientBLAZOR.Dtos;

namespace clientBLAZOR.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductDto>> GetCatalogs();
        Task<ProductDto> GetCatalog(int id);
        Task<bool> DeleteCatalog(int id);
        Task<ProductDto> CreateCatalog(ProductDto catalog);
        Task<ProductDto> UpdateCatalog(ProductDto catalog);
    }
}
