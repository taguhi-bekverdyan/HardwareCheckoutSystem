using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareCheckoutSystemWebApi.Helpers
{
    class ValidGuidAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' does not contain a valid guid";

        public ValidGuidAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = Convert.ToString(value, CultureInfo.CurrentCulture);

            // let the Required attribute take care of this validation
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            Guid guid;
            if (!Guid.TryParse(input, out guid))
            {
                // not a validstring representation of a guid
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new List<string> { validationContext.DisplayName });
            }

            //guid = (Guid)value;

            guid = new Guid(value.ToString());
            if (guid == default(Guid))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new List<string> { validationContext.DisplayName });
            }

            return ValidationResult.Success;
        }
    }
}
