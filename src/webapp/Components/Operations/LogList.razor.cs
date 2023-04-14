using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Modules;

namespace TheFipster.Zomboid.ServerControl.Components.Operations
{
    public partial class LogList
    {
        private string[] logs { get; set; } = new[] { Messages.DefaultLogMessage };
        private DockerLogPuller? logPuller;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            logPuller = new DockerLogPuller(DockerInterop, 500, 50);
            logPuller.LogsPulled += onLogsPulledAsync;
        }

        public void Start() => logPuller?.Start();

        private async Task onLogsPulledAsync(object sender, string[] logs)
        {
            await InvokeAsync(async () =>
            {
                if (logs.Any())
                    this.logs = logs;

                StateHasChanged();

                if (logs.Any(x => x.Contains(Const.LogReadyMessagePart)))
                    await onShowModsClickAsync();
            });
        }


        private async Task onShowModsClickAsync()
        {
            logPuller?.Stop();
            await JsRuntime.InvokeVoidAsync(JsMethods.ShowMods);
            logs = new[] { Messages.DefaultLogMessage };
        }
    }
}
