namespace TheFipster.Zomboid.ServerControl.Config
{
    public class SandboxSettings
    {
        public const string SectionName = "SandboxSettings";

        public List<SettingsEntry> Entries { get; set; }

        public SandboxSettings() => Entries = new();
    }
}
