using clientBLAZOR.Dtos;
using clientBLAZOR.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace clientBLAZOR.Pages
{
    public partial class OrderDetails
    {
        [Inject]
        public IOrderService orderService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? Id { get; set; }
        public string BtnText { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }

        protected OrderDto OrderDto = new OrderDto();

        protected override void OnInitialized()
        {
            BtnText = Id == null ? "Save new order" : "Update Order";
        }

        // protected override async Task OnParametersSetAsync()
        // {
        //     try
        //     {
        //         if (Id.HasValue)
        //         {
        //             OrderDto = await orderService.GetOrder(Id.Value);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         ErrorMessage = ex.Message;
        //     }
        // }

        protected async Task HandleSubmit()
        {
            await orderService.CreateOrder(OrderDto);
            NavigationManager.NavigateTo("orders");
            //try
            //{
            //    //if (Id.HasValue)
            //    //{
            //    //    // await orderService.UpdateOrder(OrderDto);
            //    //}
            //    //else
            //    //{
            //    //    await orderService.CreateOrder(OrderDto);
            //    //}
            //    await orderService.CreateOrder(OrderDto);
            //    NavigationManager.NavigateTo("orders");
            //}
            //catch (Exception ex)
            //{
            //    ErrorMessage = ex.Message;
            //}
        }

        protected async Task DeleteCatalog()
        {
            var deleted = await orderService.DeleteOrder(OrderDto.Id);
            if (deleted)
            {
                NavigationManager.NavigateTo("orders");
            }
        }
    }
}
