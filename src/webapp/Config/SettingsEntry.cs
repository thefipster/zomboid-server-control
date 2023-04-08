namespace TheFipster.Zomboid.ServerControl.Config
{
    public class SettingsEntry
    {
        public string Label { get; set; }
        public string Short { get; set; }
        public string Type { get; set; }
        public string Key { get; set; }
        public string Default { get; set; }
        public SettingsLimit Limits { get; set; }

        public SettingsEntry()
        {
            Label = string.Empty;
            Short = string.Empty;
            Type = string.Empty;
            Key = string.Empty;
            Default = string.Empty;
            Limits = new();
        }
    }
}
