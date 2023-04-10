using Microsoft.JSInterop;
using System.Timers;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.General
{
    public partial class StatusBar
    {
        private readonly System.Timers.Timer pingTimer = new(1000);
        private readonly StatusBarModel model = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            pingTimer.Elapsed += pingDockerLoop;
            pingTimer.Start();

            model.ServerIniFilename = FileService.GetIniName();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                var dotNetReference = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync(JsMethods.SyncInstances, dotNetReference);
            }
        }

        private async void pingDockerLoop(object? sender, ElapsedEventArgs e)
        {
            model.IsReady = true;
            model.IsDockerActive = await DockerService.PingAsync();

            if (!model.IsDockerActive)
                model.SetDockerStopped();
            else
                await probeContainerAsync();

            await InvokeAsync(StateHasChanged);
        }

        private async Task probeContainerAsync()
        {
            var id = await DockerService.GetLinkedIdAsync(16);
            model.SetContainerFound(id);

            if (model.IsContainerLinked)
                await setContainerStateAsync();
        }

        private async Task setContainerStateAsync()
        {
            model.ContainerState = await DockerService.GetLinkedStateAsync();
            model.ContainerStatus = await DockerService.GetLinkedStatusAsync();
        }
    }
}
