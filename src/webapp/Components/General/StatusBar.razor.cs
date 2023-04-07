using System.Timers;
using TheFipster.Zomboid.ServerControl.Models;
using Timer = System.Timers.Timer;

namespace TheFipster.Zomboid.ServerControl.Components.General
{
    public partial class StatusBar
    {
        readonly Timer pingTimer = new(1000);
        readonly StatusBarModel model = new();

        protected override void OnInitialized()
        {
            pingTimer.Elapsed += pingDockerLoop;
            pingTimer.Start();

            model.ServerIniFilename = ServerConfig.GetIniFilename();
        }

        private async void pingDockerLoop(object? sender, ElapsedEventArgs e)
        {
            model.IsReady = true;
            model.IsDockerActive = await DockerInterop.PingAsync();

            if (!model.IsDockerActive)
                model.SetDockerStopped();
            else
                await probeContainerAsync();

            await InvokeAsync(StateHasChanged);
        }

        private async Task probeContainerAsync()
        {
            var id = await DockerInterop.GetLinkedIdAsync(16);
            model.SetContainerFound(id);

            if (model.IsContainerLinked)
                await setContainerStateAsync();
        }

        private async Task setContainerStateAsync()
        {
            model.ContainerState = await DockerInterop.GetLinkedStateAsync();
            model.ContainerStatus = await DockerInterop.GetLinkedStatusAsync();
        }
    }
}
