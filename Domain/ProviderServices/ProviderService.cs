using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProviderServices
{
    public class ProviderService : BaseEntity<int>
    {
        public ProviderService()
        {
        }

        [Required]
        public int IDProvider { get; set; }

        [Required]
        public int IDService { get; set; }

        [Required]
        public decimal PriceHour { get; set; }

        [Required]
        public string CountryISO { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (IDProvider == 0)
            {
                yield return new ValidationResult(
                    "Privider Not Zero",
                    new[] { nameof(IDProvider) });
            }
            if (IDService == 0)
            {
                yield return new ValidationResult(
                    "Service Not Zero",
                    new[] { nameof(IDProvider) });
            }
        }
    }
}