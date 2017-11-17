namespace CameraBazaar.Models.Extensions.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class DividableMaxIsoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && (int)value % 100 == 0)
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Max Iso must be dividable by '100'";
        }
    }
}
