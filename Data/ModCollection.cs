namespace zomboid_server_control.Data
{
    public class ModCollection
    {
        private List<ModConfig> mods;

        public ModCollection()
            => mods = new List<ModConfig>();

        public ModCollection(IEnumerable<ModConfig> mods)
            : this()
            => this.mods.AddRange(mods);

        public string ExportModsString => 
            AppSettings.ModsPrefix 
            + string.Join(';', 
                mods
                .OrderBy(x => x.Order)
                .Select(x => x.Id));

        public string ExportWorkshopString => 
            AppSettings.WorkshopItemPrefix 
            + string.Join(';', 
                mods
                .OrderBy(x => x.Order)
                .Select(x => x.WorkshopId));
    }
}
