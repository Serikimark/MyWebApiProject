using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyNewApiProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public ICollection<UserTask>? UserTasks { get; set; }  // Navigation property to UserTasks
    
        public User()
        {
          Name = string.Empty; // Default value for Name
          Email = string.Empty; // Default value for Email
        }
    
    
    }
}
