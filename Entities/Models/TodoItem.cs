using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.API.Entities.Models
{
    public abstract class TodoItem
    {
        public Guid Id { get; protected set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Range(0.00,99999.99)]
        public decimal Amount { get; set; }
    }

}