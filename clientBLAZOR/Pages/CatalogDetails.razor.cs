using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace clientBLAZOR.Pages
{
    public partial class CatalogDetails  
    {
        [Inject]
        public ICatalogService CatalogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? Id { get; set; }
        public string BtnText { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }

        protected ProductDto Catalog = new ProductDto();

        protected override void OnInitialized()
        {
            BtnText = Id == null ? "Save new Catalog" : "Update Catalog";
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                if (Id.HasValue)
                {
                    Catalog = await CatalogService.GetCatalog(Id.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task HandleSubmit()
        {
            try
            {
                if (Id.HasValue)
                {
                    await CatalogService.UpdateCatalog(Catalog);
                }
                else
                {
                    await CatalogService.CreateCatalog(Catalog);
                }

                NavigationManager.NavigateTo("catalogs");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCatalog()
        {
            var deleted = await CatalogService.DeleteCatalog(Catalog.Id);
            if (deleted)
            {
                NavigationManager.NavigateTo("catalogs");
            }
        }
    }
}
