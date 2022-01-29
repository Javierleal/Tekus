using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Extentions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Providers
{
    public class Provider : BaseEntity<int>
    {
        public Provider()
        {
        }
        [Required]
        public string NIT { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (Name == null)
            {
                yield return new ValidationResult(
                    "Name Not NULL",
                    new[] { nameof(Name) });
            }
            if (Name == "")
            {
                yield return new ValidationResult(
                    "Name Not Empty",
                    new[] { nameof(Name) });
            }
            if (!NIT.ValidNit())
            {
                yield return new ValidationResult(
                    "NIT Not valid, null or empty",
                    new[] { nameof(NIT) });
            }
            if (!Email.ValidMail())
            {
                yield return new ValidationResult(
                    "Email Not valid, null or empty",
                    new[] { nameof(Email) });
            }
        }


    }
}