using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class ServerConfigService
    {
        private AppSettings settings;

        public ServerConfigService(AppSettings settings)
        {
            this.settings = settings;
        }

        public string GetIniFilename()
        {
            var serverPath = Path.Combine(settings.ZOMBOID_PATH, AppSettings.ServerFolder);
            var iniFile = getLatestIniFile(serverPath, AppSettings.IniFilter);


            if (string.IsNullOrWhiteSpace(iniFile))
                throw new Exception("Couldn't fine ini file.");

            var file = new FileInfo(iniFile);
            return file.Name;
        }

        public Task<ModCollection> GetMods()
        {
            throw new NotImplementedException();

            string? latest = getLatestIniFile(settings.ZOMBOID_PATH, AppSettings.IniFilter);
            if (latest == null)
                return Task.FromResult(new ModCollection());

            ModCollection config = extractIdsFromIniFile(latest);
            return Task.FromResult(config);
        }

        public void SetMods(ModCollection config)
        {
            var serverPath = Path.Combine(settings.ZOMBOID_PATH, AppSettings.ServerFolder);
            var iniFile = getLatestIniFile(serverPath, AppSettings.IniFilter);

            var modString = config.ExportModsString;
            replaceString(iniFile, AppSettings.ModsPrefix, modString);

            var workshopString = config.ExportWorkshopString;
            replaceString(iniFile, AppSettings.WorkshopItemPrefix, workshopString);
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

            var modIds = lines.FirstOrDefault(x => x.StartsWith(AppSettings.ModsPrefix));
            var workShopIds = lines.FirstOrDefault(x => x.StartsWith(AppSettings.WorkshopItemPrefix));

            if (modIds == null || workShopIds == null)
                throw new Exception("Modsettings not found in ini file.");

            return new ModCollection();
        }
    }
}