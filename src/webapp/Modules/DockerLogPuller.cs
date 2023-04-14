using System.Timers;
using TheFipster.Zomboid.ServerControl.Services;
using Timer = System.Timers.Timer;

namespace TheFipster.Zomboid.ServerControl.Modules
{
    public class DockerLogPuller
    {
        private readonly DockerInteropService interop;
        private readonly TimeSpan interval;
        private readonly int count;
        private readonly Timer timer;

        public delegate Task LogsPulledEventHandler(object sender, string[] logs);
        public event LogsPulledEventHandler? LogsPulled;

        public DockerLogPuller(DockerInteropService dockerInterop, int intervalMs, int messageCount)
        {
            interop = dockerInterop;
            interval = TimeSpan.FromMilliseconds(intervalMs);
            count = messageCount;

            timer = new Timer(interval);
            timer.Elapsed += PullLogs;
        }

        public void Start() => timer.Start();

        public void Stop() => timer.Stop();

        private async void PullLogs(object? sender, ElapsedEventArgs e)
        {
            var logs = await interop.GetLogsAsync(count);
            var lines = logs.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            LogsPulled?.Invoke(this, lines);
        }
    }
}
