using System.Timers;

namespace TheFipster.Zomboid.ServerControl.Data
{
    public class ConfirmTimer
    {
        private int windowMs = 5000;
        private int limit = 3;
        private int clicks = 0;

        System.Timers.Timer timer;

        public delegate Task ResultReachedEventHandler(object sender, bool isConfirmed);
        public event ResultReachedEventHandler ResultReached;

        public ConfirmTimer(int windowMs, int limit)
        {
            this.windowMs = windowMs;
            this.limit = limit;

            Reset();
        }

        public bool Confirmed => clicks > limit;

        public void Start()
        {
            timer = new System.Timers.Timer(windowMs);
            timer.Elapsed += callback;
            timer.Start();
        }

        public int ClicksLeft => limit - clicks;

        public int ClickCount => clicks;

        public void Click()
        {
            Reset();
            clicks++;

            if (!Confirmed)
                Start();

            if (Confirmed)
                invoke(true);

        }

        public void Reset()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Elapsed -= callback;
                timer.Dispose();
            }
        }

        private void callback(object? sender, ElapsedEventArgs e) => invoke(false);

        private void invoke(bool isConfirmed)
        {
            ResultReached?.Invoke(this, isConfirmed);
            clicks = 0;
        }
    }
}
