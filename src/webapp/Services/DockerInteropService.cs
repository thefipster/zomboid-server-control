using Docker.DotNet;
using Docker.DotNet.Models;
using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class DockerInteropService
    {
        private readonly DockerClient client;

        public DockerInteropService(AppSettings settings, IConfiguration config)
        {
            if (string.IsNullOrEmpty(settings.DOCKER_SOCKET))
                throw new Exception("Docker socket to set in settings.");

            client = new DockerClientConfiguration(
                new Uri(settings.DOCKER_SOCKET))
                 .CreateClient();
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                await client.System.PingAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task RestartAsync()
        {
            var container = await GetLinkedIdAsync();
            if (string.IsNullOrWhiteSpace(container))
                return;

            await client.Containers.RestartContainerAsync(container, new ContainerRestartParameters
            {
                WaitBeforeKillSeconds = 30
            });
        }

        public async Task<string> GetLogsAsync(int lines)
        {
            var container = await GetLinkedIdAsync();
            if (string.IsNullOrWhiteSpace(container))
                throw new Exception("Couldn't find enabled container.");

            var stream = await client.Containers.GetContainerLogsAsync(container, false, new ContainerLogsParameters
            {
                Timestamps = true,
                Tail = lines.ToString(),
                ShowStdout = true
            });

            (string stdout, string _) = await stream.ReadOutputToEndAsync(new CancellationToken());
            return stdout;
        }

        public async Task<string?> GetLinkedIdAsync(int? resultLength = null)
        {
            var container = await getContainerInfoAsync();
            if (container != null)
                return resultLength.HasValue ? container.ID[..resultLength.Value] : container.ID;

            return null;
        }

        public async Task<string?> GetLinkedStateAsync()
        {
            var container = await getContainerInfoAsync();
            return container?.State;
        }

        public async Task<string?> GetLinkedStatusAsync()
        {
            var container = await getContainerInfoAsync();
            return container?.Status;
        }

        private async Task<ContainerListResponse?> getContainerInfoAsync()
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters());
            foreach (var container in containers)
            {
                container.Labels.TryGetValue(Const.ControlLabel, out var label);
                if (!string.IsNullOrWhiteSpace(label) && label.ToLower() == "true")
                    return container;
            }

            return null;
        }
    }
}