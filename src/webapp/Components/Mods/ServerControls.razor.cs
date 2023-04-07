﻿using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Data;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ServerControls
    {
        private readonly ConfirmTimer timer = new(2000, 2);

        public string btnText = Messages.DefaultRestartButtonText;

        [Parameter]
        public EventCallback OnRestartConfirmed { get; set; }

        protected override void OnInitialized()
        {
            timer.ResultReached += onRestartTriggerAsync;
        }

        private void onRestartClick()
        {
            btnText = string.Format(Messages.ConfirmRestartButtonTemplate, timer.ClicksLeft);
            timer.Click();
        }

        private async Task onRestartTriggerAsync(object? sender, bool isConfirmed)
        {
            timer.Reset();
            btnText = Messages.DefaultRestartButtonText;

            await InvokeAsync(() => { StateHasChanged(); });

            if (isConfirmed)
                await OnRestartConfirmed.InvokeAsync();
        }
    }
}
