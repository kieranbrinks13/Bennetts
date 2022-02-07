using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BennettsDataModels
{
    public class UserDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required, Display(Name = "First Name", Description = "First name must be less than 20 characters and must not contain characters: -[]*!"), MaxLength(20), RegularExpression("^[^-*!\\[\\]]+$")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name", Description = "Last name must be less than 20 characters and must not contain characters: -[]*!"), MaxLength(20), RegularExpression("^[^-*!\\[\\]]+$")]
        public string LastName { get; set; }

        [Required, Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true), MinimumAge(18)]
        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        [NotMapped]
        public string? FullName { get
            {
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                    return $"{FirstName} {LastName}";

                return null;
            }
        }
    }

    public class MinimumAge : ValidationAttribute
    {
        private int _minimumAge;
        public MinimumAge(int minimumAge)
        {
            _minimumAge = minimumAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            var dateValue = DateTime.Parse(value.ToString());
            var today = DateTime.Today;
            int age = today.Year - dateValue.Year;
            if (dateValue > today.AddYears(age))
                age--;

            if (age < _minimumAge)
                return new ValidationResult("You must be over the age of 18.");

            return null;
        }
    }
}