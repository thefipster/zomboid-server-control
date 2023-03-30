namespace zomboid_server_control.Data
{
    public class ServerConfigService
    {
        private const string ConfigLocation = @"E:\zomboid\server\server-control-test\config\Server";

        public Task<ModCollection> GetModsAsync()
        {
            string? latest = getLatestInitFile();
            if (latest == null)
                return Task.FromResult(new ModCollection());

            ModCollection config = extractIdsFromIniFile(latest);
            return Task.FromResult(config);
        }

        public Task SetModsAsync(ModCollection config)
        {
            throw new NotImplementedException();
        }

        private string? getLatestInitFile()
        {
            var files = Directory.GetFiles(ConfigLocation, "zomboid*.ini");
            return files.OrderByDescending(name => name).FirstOrDefault();
        }

        private ModCollection extractIdsFromIniFile(string latest)
        {
            var lines = File.ReadAllLines(latest);

            var modIds = lines.FirstOrDefault(x => x.StartsWith(ModCollection.ModsPrefix));
            var workShopIds = lines.FirstOrDefault(x => x.StartsWith(ModCollection.WorkshopItemPrefix));

            if (modIds == null || workShopIds == null)
                throw new Exception("Modsettings not found in ini file.");

            return new ModCollection();
        }
    }
}