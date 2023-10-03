using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AddressBook.validator
{
    internal class TelValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
            {
                return new ValidationResult(false, "値がNullです");
            }
            string? tel = value.ToString();
            if (tel != null)
            {
                tel = tel.Replace("-", "");
                if (Regex.IsMatch(tel, @"^[0-9]{11}$") || Regex.IsMatch(tel, @"^[0-9]{10}$"))
                {
                    return ValidationResult.ValidResult;
                }
                if (tel.Length == 0)
                {
                    return new ValidationResult(false, "この項目は必須です");
                }
            }

            return new ValidationResult(false, "値の形式が不正です");
        }
    }
}
