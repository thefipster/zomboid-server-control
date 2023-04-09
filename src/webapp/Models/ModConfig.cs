namespace TheFipster.Zomboid.ServerControl.Models
{
    public class ModConfig
    {
        public ModConfig()
        {
            WorkshopId = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
        }

        public ModConfig(string? id, string? workshopId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(workshopId))
                throw new ArgumentException("Mod infos must be complete.");

            WorkshopId = workshopId;
            Id = id;
        }

        public ModConfig(string? id, string? workshopId, string? name)
            : this(id, workshopId)
            => Name = name;

        public ModConfig(string? id, string? workshopId, string? name, int order)
            : this(id, workshopId, name)
            => Order = order;

        public ModConfig(ModFormData formData)
            : this(formData.ModId, formData.WorkshopId, formData.ModName)
        { }

        public int Order { get; set; }
        public string WorkshopId { get; set; }
        public string Id { get; set; }
        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as ModConfig;
            if (other == null)
                return false;

            return other.WorkshopId == this.WorkshopId;
        }
    }
}
