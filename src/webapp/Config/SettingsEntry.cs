namespace TheFipster.Zomboid.ServerControl.Config
{
    public class SettingsEntry
    {
        public string Label { get; set; }
        public string Short { get; set; }
        public string Type { get; set; }
        public string Key { get; set; }
        public string Default { get; set; }
        public SettingsLimit Limit { get; set; }
    }
}
