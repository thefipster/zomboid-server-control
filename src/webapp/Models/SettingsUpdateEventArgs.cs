namespace TheFipster.Zomboid.ServerControl.Models
{
    public class SettingsUpdateEventArgs
    {
        public string Value { get; set; }
        public string Property { get; set; }

        public SettingsUpdateEventArgs(string value, string prop)
        {
            Value = value;
            Property = prop;
        }
    }
}
