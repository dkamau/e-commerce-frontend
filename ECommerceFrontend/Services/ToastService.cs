using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ECommerceFrontend.Services
{
    public class ToastService
    {
        protected readonly IJSRuntime _jsRuntime;
        public ToastService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowToastAsync(string message, ToastType toastType)
        {
            await _jsRuntime.InvokeVoidAsync("showSwalToast", message, toastType.ToString().ToLower());
        }
    }

    public enum ToastType
    {
        Success,
        Info,
        Error,
        Warning,
        Question
    }
}
