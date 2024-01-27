using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.API.Entities.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;

        [Range(0.00, 99999.99)] // or [Precision(18,2)] // forcing two decimal place and max amount to transfer 
        decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Completed { get; set; }

        public string? FF { get; set; }
    }
}

/*
    Indicates that a property is the primary key.
    [Key]

    Specifies that a property must have a value.
    [Required]

    Specifies the maximum and minimum length of a string property.
    [StringLength(50, MinimumLength = 2)]

    Specifies the range of values for a numeric property.
    [Range(1, 100)]

    Validates that the value of the property matches a regular expression.
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]

    Validates that the value of the property is a valid email address.
    [EmailAddress]

    Specifies the data type of the property.
    [DataType(DataType.Date)]

    Provides additional information about how to display the property in UI.
    [Display(Name = "Product Name")]
*/
