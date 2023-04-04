using System.Timers;
using TheFipster.Zomboid.ServerControl.Services;
using Timer = System.Timers.Timer;

namespace TheFipster.Zomboid.ServerControl.Data
{
    public class DockerLogPuller
    {
        private DockerInteropService interop;
        private Timer timer;

        public delegate Task LogsPulledEventHandler(object sender, string[] logs);
        public event LogsPulledEventHandler LogsPulled;

        public DockerLogPuller(DockerInteropService interop)
        {
            this.interop = interop;

            timer = new Timer(500);
            timer.Elapsed += PullLogs;
        }

        public void Start() => timer.Start();

        public void Stop() => timer.Stop();

        private async void PullLogs(object? sender, ElapsedEventArgs e)
        {
            var logs = await interop.GetLogsAsync(50);
            var lines = logs.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            LogsPulled?.Invoke(this, lines);
        }
    }
}
