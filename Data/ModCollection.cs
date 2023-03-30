namespace zomboid_server_control.Data
{
    public class ModCollection
    {
        public const string ModsPrefix = "Mods=";
        public const string WorkshopItemPrefix = "WorkshopItems=";

        private List<ModConfig> modConfigs;

        public ModCollection()
        {
            modConfigs = new List<ModConfig>();
        }

        public ModCollection(IEnumerable<ModConfig> mods)
            : this()
        {
            modConfigs.AddRange(mods);
        }
    }
}
