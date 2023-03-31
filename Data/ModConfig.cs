namespace zomboid_server_control.Data
{
    public class ModConfig
    {
        public ModConfig()
        {
            WorkshopId = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
        }

        public ModConfig(string? id, string? workshopId, string? name, int order)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(workshopId) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Mod infos must be complete.");

            Id = id;
            WorkshopId = workshopId;
            Name = name;
            Order = order;
        }

        public ModConfig(ModFormData formData, int order)
            : this(formData.ModId, formData.WorkshopId, formData.ModName, order)
        { }

        public int Order { get; set; }
        public string WorkshopId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
