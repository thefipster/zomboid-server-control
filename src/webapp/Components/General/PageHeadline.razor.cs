using Microsoft.JSInterop;

namespace TheFipster.Zomboid.ServerControl.Components.General
{
    public partial class PageHeadline
    {
        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("interop.focus");
            }
        }
    }
}
