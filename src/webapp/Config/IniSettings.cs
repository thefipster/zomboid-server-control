namespace TheFipster.Zomboid.ServerControl.Config
{
    public class IniSettings
    {
        public const string SectionName = "IniSettings";

        public List<SettingsEntry> Entries { get; set; }

        public IniSettings() => Entries = new();
    }
}
