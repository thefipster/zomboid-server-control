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

        public ModConfig(string? id, string? workshopId, string? name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(workshopId) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Mod infos must be complete.");

            Id = id;
            WorkshopId = workshopId;
            Name = name;
        }

        public ModConfig(string? id, string? workshopId, string? name, int order)
            : this(id, workshopId, name)
            => Order = order;

        public ModConfig(ModFormData formData, int order)
            : this(formData.ModId, formData.WorkshopId, formData.ModName, order)
        { }

        public ModConfig(ModFormData formData)
            : this(formData.ModId, formData.WorkshopId, formData.ModName)
        { }

        public int Order { get; set; }
        public string WorkshopId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
