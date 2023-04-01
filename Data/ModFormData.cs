using System.ComponentModel.DataAnnotations;

namespace TheFipster.Zomboid.ServerControl.Data
{
    public class ModFormData
    {
        public ModFormData() => Reset();

        [Required(ErrorMessage = "Mod ID is required.")]
        public string? ModId { get; set; }

        [Required(ErrorMessage = "Workshop Name is required.")]
        public string? ModName { get; set; }

        [Required(ErrorMessage = "Workshop Item is required.")]
        [StringIsOnlyDigits(ErrorMessage = "Workshop Item must be digits.")]
        public string? WorkshopId { get; set; }

        public void Reset()
        {
            ModId = string.Empty; 
            ModName = string.Empty;
            WorkshopId = string.Empty;
        }
    }
}
