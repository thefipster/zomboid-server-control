using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Controls
{
    public partial class CoordinateSetting
    {
        private double x;
        private double y;
        private double floor;
        private bool valueSet;

        public const string SettingsType = "CoordinateSetting";

        [Parameter]
        public SettingsEntry Setting { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (valueSet || Setting == null)
                return;

            valueSet = true;

            var coordinates = Setting.Active;
            var tokens = coordinates.Split(',');
            if (tokens.Length != 3)
                throw new ArgumentException("Coordinate setting must consist of three elements.");

            if (double.TryParse(tokens[0], out var parsedX))
                x = parsedX;

            if (double.TryParse(tokens[1], out var parsedY))
                y = parsedY;

            if (double.TryParse(tokens[2], out var parsedFloor))
                floor = parsedFloor;
        }

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        private async Task xChanged(ChangeEventArgs e)
        {
            if (!double.TryParse(e.Value.ToString(), out var number))
                return;

            if (number < 0)
                number = 0;

            x = number;

            var value = createValue();
            var args = new SettingsUpdateEventArgs(value, Setting.Name);
            await OnValueChanged.InvokeAsync(args);
        }

        private async Task yChanged(ChangeEventArgs e)
        {
            if (!double.TryParse(e.Value.ToString(), out var number))
                return;

            if (number < 0)
                number = 0;

            y = number;

            var value = createValue();
            var args = new SettingsUpdateEventArgs(value, Setting.Name);
            await OnValueChanged.InvokeAsync(args);
        }

        private async Task floorChanged(ChangeEventArgs e)
        {
            if (!double.TryParse(e.Value.ToString(), out var number))
                return;

            if (number < 0)
                number = 0;

            if (number > 7)
                number = 7;

            floor = number;

            var value = createValue();
            var args = new SettingsUpdateEventArgs(value, Setting.Name);
            await OnValueChanged.InvokeAsync(args);
        }

        private string createValue() => $"{x},{y},{floor}";
    }
}
