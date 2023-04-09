using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class IniSettingsService
    {
        private readonly IniFileService fileService;
        private readonly IniSettings settings;

        public IniSettingsService(IniFileService fileService, IniSettings settings)
        {
            this.fileService = fileService;
            this.settings = settings;
        }

        public Dictionary<string, string> GetSettings() 
        {
            var iniPath = fileService.GetIniPath();
            var lines = File.ReadAllLines(iniPath);
            var selection = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                foreach (var setting in settings.Entries)
                {
                    if (line.StartsWith(setting.Name))
                    {
                        addSettingToDictionary(selection, line);
                        break;
                    }
                }
            }

            return selection;
        }

        private void addSettingToDictionary(Dictionary<string, string> selection, string line)
        {
            var firstEqualIndex = line.IndexOf('=');

            var name = line.Substring(0, firstEqualIndex);
            var value = line.Substring(firstEqualIndex + 1);

            selection.Add(name, value);
        }
    }
}
