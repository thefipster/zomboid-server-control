namespace zomboid_server_control.Data
{
    public class AppSettings
    {
        public const string ModsPrefix = "Mods=";

        public const string WorkshopItemPrefix = "WorkshopItems=";

        public const string IniFilter = "zomboid*.ini";

        public const string ServerFolder = "Server";

        public const string DatabaseName = "mods.db";

        public const string ControlLabel = "com.thefipster.zomboid.control.enabled";

        public string? DATABASE_PATH { get; set; }

        public string? ZOMBOID_PATH { get; set; }

        public string? DOCKER_SOCKET { get; set; }
    }
}
