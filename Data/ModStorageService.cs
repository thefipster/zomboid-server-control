using LiteDB;

namespace zomboid_server_control.Data
{
    public class ModStorageService
    {
        private LiteDatabase database;

        public ModStorageService()
        {
            database = new LiteDatabase(@"E:\zomboid\server\server-control-test\mods.db");
        }

        public IEnumerable<ModConfig> Read() => database.GetCollection<ModConfig>().FindAll();

        public void Write(IEnumerable<ModConfig> mods)
        {
            database.GetCollection<ModConfig>().DeleteAll();
            database.GetCollection<ModConfig>().InsertBulk(mods);
        }
    }
}
