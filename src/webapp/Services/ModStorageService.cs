using LiteDB;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class ModStorageService
    {
        private ILiteCollection<ModConfig> collection;

        public ModStorageService(LiteDatabaseService service)
            => collection = service.Database.GetCollection<ModConfig>();

        public IEnumerable<ModConfig> Get() => collection
            .FindAll()
            .OrderBy(x => x.Order);

        public void Set(IEnumerable<ModConfig> mods)
        {
            collection.DeleteAll();
            collection.InsertBulk(mods);
        }
    }
}
