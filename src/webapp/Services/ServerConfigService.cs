using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class ServerConfigService
    {
        private readonly string zomboidPath;

        public ServerConfigService(AppSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ZOMBOID_PATH))
                throw new Exception("Couldn't read zomboid path from config.");

            zomboidPath = settings.ZOMBOID_PATH;
        }

        public string GetIniFilename()
        {
            var serverPath = Path.Combine(zomboidPath, Const.ServerFolder);
            var iniFile = getLatestIniFile(serverPath, Const.IniFilter);


            if (string.IsNullOrWhiteSpace(iniFile))
                throw new Exception($"Couldn't fine ini file at {serverPath}.");

            var file = new FileInfo(iniFile);
            return file.Name;
        }

        public Task<ModCollection> GetMods()
        {
            throw new NotImplementedException();

            string? latest = getLatestIniFile(zomboidPath, Const.IniFilter);
            if (latest == null)
                return Task.FromResult(new ModCollection());

            ModCollection config = extractIdsFromIniFile(latest);
            return Task.FromResult(config);
        }

        public void SetMods(ModCollection config)
        {
            var serverPath = Path.Combine(zomboidPath, Const.ServerFolder);
            var iniFile = getLatestIniFile(serverPath, Const.IniFilter);

            if (string.IsNullOrWhiteSpace(iniFile))
                throw new Exception($"Couldn't fine ini file at {serverPath}.");

            var modString = config.ExportModsString;
            replaceString(iniFile, Const.ModsPrefix, modString);

            var workshopString = config.ExportWorkshopString;
            replaceString(iniFile, Const.WorkshopItemPrefix, workshopString);
        }

        private void replaceString(string file, string filter, string replacement)
        {
            if (!File.Exists(file))
                throw new ArgumentException($"Zomboid ini file {file} not existing.");

            var lines = File.ReadAllLines(file).ToList();
            var index = lines.FindIndex(x => x.StartsWith(filter));
            lines[index] = replacement;

            File.WriteAllLines(file, lines);
        }

        private string? getLatestIniFile(string path, string filter) =>
            Directory
            .GetFiles(path, filter)
            .OrderByDescending(name => name)
            .FirstOrDefault();

        private ModCollection extractIdsFromIniFile(string latest)
        {
            var lines = File.ReadAllLines(latest);

            var modIds = lines.FirstOrDefault(x => x.StartsWith(Const.ModsPrefix));
            var workShopIds = lines.FirstOrDefault(x => x.StartsWith(Const.WorkshopItemPrefix));

            if (modIds == null || workShopIds == null)
                throw new Exception("Modsettings not found in ini file.");

            return new ModCollection();
        }
    }
}