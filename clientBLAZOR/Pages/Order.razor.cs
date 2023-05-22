using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace clientBLAZOR.Pages
{
    public class Ordeer : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IOrderService orderService { get; set; }

        public IEnumerable<OrderDto>? orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            orders = await orderService.GetOrders();
        }

        protected void CreateNewOrder()
        {
            NavigationManager.NavigateTo("order");
        }

        protected void ShowCatalog(int id)
        {
            NavigationManager.NavigateTo($"catalog/{id}");
        }

        protected async Task DeleteCatalog(int id)
        {
            await orderService.DeleteOrder(id);
            NavigationManager.NavigateTo("catalogs");

        }
    }
}
