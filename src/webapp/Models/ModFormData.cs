using System.ComponentModel.DataAnnotations;
using TheFipster.Zomboid.ServerControl.Attributes;
using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Models
{
    public class ModFormData
    {
        public ModFormData() => Reset();

        [Required(ErrorMessage = Messages.ModIdRequiredErrorMessage)]
        public string? ModId { get; set; }

        [Required(ErrorMessage = Messages.WorkshopNameRequiredErrorMessage)]
        public string? ModName { get; set; }

        [Required(ErrorMessage = Messages.WorkshopItemRequiredErrorMessage)]
        [StringIsOnlyDigits(ErrorMessage = Messages.WorkshopItemOnlyDigitsErrorMessage)]
        public string? WorkshopId { get; set; }

        public void Reset()
        {
            ModId = string.Empty;
            ModName = string.Empty;
            WorkshopId = string.Empty;
        }
    }
}
