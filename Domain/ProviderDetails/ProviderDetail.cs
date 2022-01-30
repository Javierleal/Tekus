using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProviderDetails
{
    public class ProviderDetail : BaseEntity<int>
    {
        public ProviderDetail()
        {
        }

        [Required]
        public int IDProvider { get; set; }

        [Required]
        public string RowName { get; set; }

        [Required]
        public string RowValue { get; set; }


        public IEnumerable<ValidationResult> Validate()
        {
            if (IDProvider == 0)
            {
                yield return new ValidationResult(
                    "Provider Not Zero",
                    new[] { nameof(IDProvider) });
            }
        }
    }
}