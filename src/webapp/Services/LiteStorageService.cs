using LiteDB;
using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Services
{
    public class LiteDatabaseService
    {
        public LiteDatabaseService(AppSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings?.DATABASE_PATH))
                throw new Exception("Database path is not specified.");

            var dbPath = Path.Combine(settings.DATABASE_PATH, Const.DatabaseName);
            Database = new LiteDatabase(dbPath);
        }

        public LiteDatabase Database { get; private set; }
    }
}
