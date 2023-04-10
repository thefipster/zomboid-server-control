using LiteDB;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class ModArchiveService
    {
        private ILiteCollection<ModArchive> collection;

        public ModArchiveService(LiteDatabaseService dbService)
            => collection = dbService.Database.GetCollection<ModArchive>();

        public IEnumerable<ModArchive> Get() => collection
            .FindAll()
            .OrderByDescending(x => x.ArchivedAt);

        public bool Exists(string workshopId) => collection
            .Exists(x => x.WorkshopId == workshopId);

        public bool Insert(ModConfig mod)
        {
            if (Exists(mod.WorkshopId))
                return true;

            var archive = new ModArchive(mod);
            return tryInsert(archive);
        }

        private bool tryInsert(ModArchive archive)
        {
            try
            {
                collection.Insert(archive);
                collection.EnsureIndex(x => x.WorkshopId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
