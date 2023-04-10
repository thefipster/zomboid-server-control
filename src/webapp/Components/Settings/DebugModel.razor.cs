using Microsoft.AspNetCore.Components;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class DebugModel
    {
        [Parameter]
        public Dictionary<string, string> Model { get; set; }

        public DebugModel()
        {
            Model = new();
        }
    }
}
