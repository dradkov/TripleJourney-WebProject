using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BlogTriple.Controllers.HotelsController;
using static BlogTriple.Models.Car;

namespace BlogTriple.Models
{
    public class Destination
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Town { get; set; }

        [Required]
        [Display(Name = "From")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "Invalid Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [Required]
        [Display(Name = "To")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "Invalid Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }

        [Required]
        [Display(Name = "Rooms")]
        public string Rooms { get; set; }

        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0} EURO")]
        public decimal Price { get; set; }

        //public string TouristId { get; set; }

        //public virtual ApplicationUser Tourist { get; set; }

       

        public class CustomDateRangeAttribute : RangeAttribute
        {
            public CustomDateRangeAttribute()
                : base(typeof(DateTime), DateTime.Now.AddDays(-1).ToString(), DateTime.Now.AddYears(20).ToString())
            {
            }
        }

        public sealed class IsDateAfter : ValidationAttribute, IClientValidatable
        {
            private readonly string testedPropertyName;
            private readonly bool allowEqualDates;

            public IsDateAfter(string testedPropertyName, bool allowEqualDates = false)
            {
                this.testedPropertyName = testedPropertyName;
                this.allowEqualDates = allowEqualDates;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var propertyTestedInfo = validationContext.ObjectType.GetProperty(this.testedPropertyName);
                if (propertyTestedInfo == null)
                {
                    return new ValidationResult(string.Format("unknown property {0}", this.testedPropertyName));
                }

                var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

                if (value == null || !(value is DateTime))
                {
                    return ValidationResult.Success;
                }

                if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
                {
                    return ValidationResult.Success;
                }

                // Compare values
                if ((DateTime)value >= (DateTime)propertyTestedValue)
                {
                    if (this.allowEqualDates)
                    {
                        return ValidationResult.Success;
                    }
                    if ((DateTime)value > (DateTime)propertyTestedValue)
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                ControllerContext context)
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = this.ErrorMessageString,
                    ValidationType = "isdateafter"
                };
                rule.ValidationParameters["propertytested"] = this.testedPropertyName;
                rule.ValidationParameters["allowequaldates"] = this.allowEqualDates;
                yield return rule;
            }
        }


    }
}