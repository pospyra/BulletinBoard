using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Otiva.Contracts.Attributs
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var pass = value as string;

            // Проверяем, содержит ли пароль хотя бы одну букву верхнего регистра, одну букву нижнего регистра и одну цифру
            var regexLetter = new Regex(@"^(?=.*[a-z])(?=.*[A-Z]).+$");
            var regexSymbol = new Regex(@"^(?=.*\d)(?=.*[~!@#$%^&*()_\-+=|{}\[\]:;""'<>,.?/]).+$"); 

            if (!regexLetter.IsMatch(pass))
            {
                return new ValidationResult("Пароль должен содержать как минимум одну строчную букву (a-z);" +
                    "Пароль должен содержать как минимум одну заглавную букву (A-Z);");
            }
            if (!regexSymbol.IsMatch(pass))
                return new ValidationResult("Пароль должен содержать как минимум одну цифру и минимум один специальный символ");


            return ValidationResult.Success;

        }
    }
}
