using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class ModConfigService
    {
        private readonly string iniPath;

        public ModConfigService(IniFileService fileService)
            => iniPath = fileService.GetIniPath();

        public IEnumerable<ModConfig> GetMods()
        {
            var workshopIds = getValues(Const.WorkshopItemPrefix, Const.IniFileValueSeparator);
            var modIds = getValues(Const.ModsPrefix, Const.IniFileValueSeparator);

            if (workshopIds.Count() != modIds.Count())
                throw new ArgumentException("Can't match workshop items with mod ids. The number of items in the 'Mods' and 'WorkshopItems' properties are different.");

            var mods = new List<ModConfig>();
            for (int i = 0; i < workshopIds.Length; i++)
            {
                var mod = new ModConfig(modIds[i], workshopIds[i]);
                mods.Add(mod);
            }

            return mods;
        }

        public void SetMods(ModCollection config)
        {
            if (string.IsNullOrWhiteSpace(iniPath))
                throw new Exception($"Couldn't fine ini file at {iniPath}.");

            var modString = config.ExportModsString;
            replaceString(Const.ModsPrefix, modString);

            var workshopString = config.ExportWorkshopString;
            replaceString(Const.WorkshopItemPrefix, workshopString);
        }

        private string[] getValues(string filter, char separator)
        {
            var line = getString(filter);

            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException($"Server ini file is missing property {filter}");

            line = line.Trim().Replace(filter, string.Empty);
            var items = line.Split(separator);
            return items;
        }

        private void replaceString(string filter, string replacement)
        {
            var lines = getAllLines().ToList();
            var index = lines.FindIndex(x => x.StartsWith(filter));
            lines[index] = replacement;

            File.WriteAllLines(iniPath, lines);
        }

        private string? getString(string filter)
            => getAllLines()
              .FirstOrDefault(line => 
               line.StartsWith(filter));

        private string[] getAllLines()
        {
            if (!File.Exists(iniPath))
                throw new ArgumentException($"Zomboid ini file {iniPath} not existing.");

            var lines = File.ReadAllLines(iniPath);
            return lines;
        }
    }
}