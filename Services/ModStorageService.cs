using LiteDB;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
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
            var collection = database.GetCollection<ModConfig>();

            collection.DeleteAll();
            collection.InsertBulk(mods);
        }

        public IEnumerable<ModArchive> Retrieve() => database.GetCollection<ModArchive>().FindAll();

        public bool Archive(ModConfig mod)
        {
            var archive = new ModArchive(mod);
            var collection = database.GetCollection<ModArchive>();

            try
            {
                collection.Insert(archive);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
