using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Models
{
    public class ModCollection
    {
        private readonly List<ModConfig> mods;

        public ModCollection()
            => mods = new List<ModConfig>();

        public ModCollection(IEnumerable<ModConfig> mods)
            : this()
            => this.mods.AddRange(mods);

        public string ExportModsString =>
            Const.ModsPrefix
            + string.Join(Const.IniFileValueSeparator,
                mods
                .OrderBy(x => x.Order)
                .Select(x => x.Id));

        public string ExportWorkshopString =>
            Const.WorkshopItemPrefix
            + string.Join(Const.IniFileValueSeparator,
                mods
                .OrderBy(x => x.Order)
                .Select(x => x.WorkshopId));
    }
}
