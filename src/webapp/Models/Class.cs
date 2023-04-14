namespace TheFipster.Zomboid.ServerControl.Models
{
    public class SettingsModel
    {
        public SettingsModel()
        {
            Values = new();
        }

        public Dictionary<string, string> Values { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
