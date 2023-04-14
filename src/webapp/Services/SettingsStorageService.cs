using LiteDB;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class SettingsStorageService
    {
        private ILiteCollection<SettingsModel> collection;

        public SettingsStorageService(LiteDatabaseService service)
            => collection = service.Database.GetCollection<SettingsModel>();

        public SettingsModel? Get() => collection
            .FindAll().FirstOrDefault();

        public void Set(SettingsModel settings)
        {
            collection.DeleteAll();
            collection.Insert(settings);
        }
    }
}
