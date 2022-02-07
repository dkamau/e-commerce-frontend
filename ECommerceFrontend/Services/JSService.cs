using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class JSService
    {
        protected readonly IJSRuntime _jsRuntime;
        public JSService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task RemoveElement(string elementId)
        {
            await _jsRuntime.InvokeVoidAsync("removeElement", elementId);
        }

        public async Task UpdateCart()
        {
            await _jsRuntime.InvokeVoidAsync("updateCart");
        }

        public async Task RemoveItemFromCart(int number = 1)
        {
            await _jsRuntime.InvokeVoidAsync("removeItemFromCart", number);
        }
    }
}
