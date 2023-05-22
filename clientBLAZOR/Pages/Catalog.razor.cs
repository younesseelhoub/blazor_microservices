using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace clientBLAZOR.Pages
{
    public  class Category : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ICatalogService CatalogService { get; set; }

        public IEnumerable<ProductDto>? Catalogs { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Catalogs = await CatalogService.GetCatalogs();
        }

        protected void CreateNewCatalog()
        {
            NavigationManager.NavigateTo("catalog");
        }

        protected void ShowCatalog(int id)
        {
            NavigationManager.NavigateTo($"catalog/{id}");
        }

        protected async Task DeleteCatalog(int id)
        {
            await CatalogService.DeleteCatalog(id);
            NavigationManager.NavigateTo("catalogs");

        }
    }
}
