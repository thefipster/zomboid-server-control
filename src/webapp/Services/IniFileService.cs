using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class IniFileService
    {
        private readonly string zomboidPath;

        public IniFileService(AppSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ZOMBOID_PATH))
                throw new ArgumentNullException("Couldn't read zomboid path from config.");

            zomboidPath = settings.ZOMBOID_PATH;
        }

        public string GetIniPath()
        {
            var serverPath = Path.Combine(zomboidPath, Const.ServerFolder);
            var iniFile = getLatestIniFile(serverPath, Const.IniFilter);

            if (string.IsNullOrWhiteSpace(iniFile))
                throw new FileNotFoundException($"Couldn't fine ini file at {serverPath}.");

            return iniFile;

            
        }

        public string GetIniName()
        {
            var iniFile = GetIniPath();
            var file = new FileInfo(iniFile);
            return file.Name;
        }

        private string? getLatestIniFile(string path, string filter) =>
            Directory
            .GetFiles(path, filter)
            .OrderByDescending(name => name)
            .FirstOrDefault();
    }
}
