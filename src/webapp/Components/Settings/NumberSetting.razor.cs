﻿using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class NumberSetting
    {
        public const string SettingsType = "NumberSetting";

        private double internalValue;

        [Parameter]
        public SettingsEntry Setting { get; set; }

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        public NumberSetting()
        {
            Setting = new();
        }

        protected override void OnParametersSet()
        {
            if (internalValue != default)
                return;

            if (double.TryParse(Setting.Default, out var number))
            {
                internalValue = number;
            }
        }

        private async Task valueChanged(ChangeEventArgs e)
        {
            if (!double.TryParse(e.Value.ToString(), out var number))
                return;

            if (number < Setting.Limits.Min)
                number = Setting.Limits.Min;

            if (number > Setting.Limits.Max)
                number = Setting.Limits.Max;

            internalValue = number;

            var args = new SettingsUpdateEventArgs(number.ToString(), Setting.Key);
            await OnValueChanged.InvokeAsync(args);
        }
    }
}
