using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Neogov.Core.Common.CustomAttributes
{
    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute(string errorMessage = "Email is not valid")
        {
            this.ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var email = value.ToString();
            if (email.Length <= 256)
                return false;

            return Regex.IsMatch(email, @"^(.+)@(.+)$");
        }
    }
}