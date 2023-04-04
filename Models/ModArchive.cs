namespace TheFipster.Zomboid.ServerControl.Models
{
    public class ModArchive
    {
        public ModArchive(ModConfig mod)
        {
            Mod = mod;
            WorkshopId = mod.WorkshopId;
            ArchivedAt = DateTime.UtcNow;
        }

        public string WorkshopId { get; set; }

        public ModConfig Mod { get; set; }

        public DateTime ArchivedAt { get; set; }
    }
}
