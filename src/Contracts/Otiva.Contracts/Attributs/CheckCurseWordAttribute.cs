using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Otiva.Contracts.Attributs
{
    public class CheckCurseWordAttribute : ValidationAttribute
    {
        private string[] WordsList = new string[]
    {
        "блядь",
        "блять",
        "ебать",
        "пизда",
        "хуй",
        "хули",
        "хуёво",
        "пошел нахуй",
        "хуй",
        "пидорас",
        "хуйло",
        "хуйлуша",
        "мразота",
        "ебаная",
        "конченный",
        "конченнная",
        "конченное",
        "ебаное"
    };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueAsString = value as string;

            if (valueAsString == null)
                return ValidationResult.Success;

            var valueContainsAnyForbiddenWord = WordsList.Any(forbiddenWord =>
                valueAsString.Contains(forbiddenWord, StringComparison.InvariantCultureIgnoreCase));

            return valueContainsAnyForbiddenWord
                 ? new ValidationResult("Значение содержит нецензурную лексику")
                 : ValidationResult.Success;
        }
    }
}
