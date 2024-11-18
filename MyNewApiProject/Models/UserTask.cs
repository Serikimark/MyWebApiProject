using System.ComponentModel.DataAnnotations;

namespace MyNewApiProject.Models
{
    public class UserTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } // Navigation Property

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Navigation Property

       public UserTask()
     {
         Title = string.Empty; // Default value for Title
         Description = string.Empty; // Default value for Description
         User = new User(); // Initialize User object
         Category = new Category(); // Initialize Category object
     }

    }
}
