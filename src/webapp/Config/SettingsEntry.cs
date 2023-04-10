namespace TheFipster.Zomboid.ServerControl.Config
{
    public class SettingsEntry
    {
        public SettingsEntry()
        {
            Type = string.Empty;
            Name = string.Empty;
            Label = string.Empty;
            Short = string.Empty;
            Info = string.Empty;
            Default = string.Empty;
            Value = string.Empty;
            Limits = new();
            Options = new List<OptionEntry>();
        }
        public string Label { get; set; }
        public string Short { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Default { get; set; }
        public string Value { get; set; }
        public SettingsLimit Limits { get; set; }
        public List<OptionEntry> Options { get; set; }

        public string Active => string.IsNullOrEmpty(Value) ? Default : Value;
    }
}
