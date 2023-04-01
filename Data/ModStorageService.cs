using LiteDB;

namespace TheFipster.Zomboid.ServerControl.Data
{
    public class ModStorageService
    {
        private LiteDatabase database;

        public ModStorageService(AppSettings settings)
        {
            if (settings == null || string.IsNullOrWhiteSpace(settings.DATABASE_PATH))
                throw new Exception("Database path is not specified.");

            var dbPath = Path.Combine(settings.DATABASE_PATH, AppSettings.DatabaseName);
            database = new LiteDatabase(dbPath);
        }

        public IEnumerable<ModConfig> Read() => database.GetCollection<ModConfig>().FindAll().OrderBy(x => x.Order);

        public void Write(IEnumerable<ModConfig> mods)
        {
            database.GetCollection<ModConfig>().DeleteAll();
            database.GetCollection<ModConfig>().InsertBulk(mods);
        }
    }
}
