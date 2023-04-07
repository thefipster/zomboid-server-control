namespace TheFipster.Zomboid.ServerControl.Config
{
    public static class Messages
    {
        // Log List
        public const string DefaultLogMessage = "Container restarted, awaiting log stream...";

        // Mod Form
        public const string DuplicateMod = "Workshop item is already in the list.";

        public const string ModIdRequiredErrorMessage = "Mod ID is required.";
        public const string WorkshopNameRequiredErrorMessage = "Workshop Name is required.";
        public const string WorkshopItemRequiredErrorMessage = "Workshop Item is required.";
        public const string WorkshopItemOnlyDigitsErrorMessage = "Workshop Item must be digits.";

        // Server Controls
        public const string DefaultRestartButtonText = "Apply & Restart";
        public const string ConfirmRestartButtonTemplate = "Click again {0}x";

        // Status Bar
        public const string NoLinkedContainerFoundText = "";
        public const string DockerDaemonOnText = "active";
        public const string DockerDaemonOffText = "stopped";
    }
}
