using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Pages
{
    public partial class Settings
    {
        public Dictionary<string, string> Model { get; set; }

        public Settings()
        {
            Model = new();
        }

        private async Task valueChanged(SettingsUpdateEventArgs e)
        {
            if (Model.ContainsKey(e.Property))
                Model[e.Property] = e.Value;
            else
                Model.Add(e.Property, e.Value);

            await InvokeAsync(StateHasChanged);
        }
    }
}
