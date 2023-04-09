using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class Overview
    {
        private async Task onClick(string anchor)
        {
            await JsRuntime.InvokeVoidAsync(JsMethods.ScrollToAnchor, "dynamic-form", anchor);
        }
    }
}
