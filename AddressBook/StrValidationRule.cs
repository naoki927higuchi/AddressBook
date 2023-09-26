using System.Globalization;
using System.Windows.Controls;

namespace AddressBook
{
    internal class StrValidationRule : ValidationRule
    {
        public int MinLength { get; set; } = int.MinValue;
        public int MaxLength { get; set; } = int.MaxValue;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
            {
                return new ValidationResult(false, "値がNullです");
            }
            string? str = value.ToString();
            if (str != null)
            {
                if (str.Length == 0)
                {
                    if (MinLength == 0)
                        return ValidationResult.ValidResult;
                    else
                        return new ValidationResult(false, "この項目は必須です");
                }
                if (str.Length < MinLength)
                {
                    return new ValidationResult(false, "値が短すぎです");
                }
                if (str.Length > MaxLength)
                {
                    return new ValidationResult(false, "値が長すぎです");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
