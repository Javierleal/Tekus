using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Services
{
    public class Service : BaseEntity<int>
    {
        public Service()
        {
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

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
        }
    }
}