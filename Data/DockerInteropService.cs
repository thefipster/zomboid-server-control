﻿using Docker.DotNet;
using Docker.DotNet.Models;

namespace zomboid_server_control.Data
{
    public class DockerInteropService
    {
        private DockerClient client;

        public DockerInteropService(AppSettings settings)
        {
            if (string.IsNullOrEmpty(settings.DOCKER_SOCKET))
                throw new Exception("Docker socket to set in settings.");

            client = new DockerClientConfiguration(
                new Uri(settings.DOCKER_SOCKET))
                 .CreateClient();
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

        public async Task<string> GetLinkedIdAsync(int? resultLength = null)
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters());
            foreach (var container in containers)
                if (container.Labels.ContainsKey(AppSettings.ControlLabel) 
                    && container.Labels[AppSettings.ControlLabel].ToLower() == "true")
                        return resultLength.HasValue ? container.ID.Substring(0, resultLength.Value) : container.ID;

            return string.Empty;
        }

        public async Task<string> GetLinkedStateAsync()
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters());
            foreach (var container in containers)
            {
                if (container.Labels.ContainsKey(AppSettings.ControlLabel))
                {
                    var value = container.Labels[AppSettings.ControlLabel];
                    if (value.ToLower() == "true")
                    {
                        return container.State;
                    }
                }
            }

            return string.Empty;
        }
    }
}