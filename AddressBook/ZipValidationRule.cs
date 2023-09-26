using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AddressBook
{
    internal class ZipValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
            {
                return new ValidationResult(false, "値がNullです");
            }
            string? zip = value.ToString();
            if (zip != null)
            {
                if (Regex.IsMatch(zip, @"^[0-9]{7}$") || Regex.IsMatch(zip, @"^[0-9]{3}-[0-9]{4}$"))
                {
                    return ValidationResult.ValidResult;
                }
                if (zip.Length == 0)
                {
                    return new ValidationResult(false, "この項目は必須です");
                }
            }

            return new ValidationResult(false, "値の形式が不正です");
        }
    }
}
