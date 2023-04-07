using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Models
{
    public class StatusBarModel
    {
        public bool IsReady { get; set; }
        public bool IsDockerActive { get; set; }
        public string? DockerStatus { get; set; }
        public bool IsContainerLinked { get; set; }
        public string? ContainerId { get; set; }
        public string? ContainerState { get; set; }
        public string? ContainerStatus { get; set; }
        public string? ServerIniFilename { get; set; }

        internal void SetContainerFound(string? containerId)
        {
            ContainerId = containerId ?? Messages.NoLinkedContainerFoundText;
            DockerStatus = Messages.DockerDaemonOnText;
            ContainerState = string.Empty;
            ContainerStatus = string.Empty;

            if (string.IsNullOrWhiteSpace(containerId))
                IsContainerLinked = false;
            else
                IsContainerLinked = true;
        }

        internal void SetDockerStopped()
        {
            DockerStatus = Messages.DockerDaemonOffText;
            ContainerId = string.Empty;
            IsContainerLinked = false;
            ContainerState = string.Empty;
            ContainerStatus = string.Empty;
        }
    }
}
