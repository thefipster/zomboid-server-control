using System.ComponentModel.DataAnnotations;

namespace TheFipster.Zomboid.ServerControl.Data
{
    public class StringIsOnlyDigitsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
                return strValue.All(c => c >= '0' && c <= '9');
            }

            return true;
        }
    }
}
