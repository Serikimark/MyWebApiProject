using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyNewApiProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<UserTask> UserTasks { get; set; }  // Navigation property to UserTasks
    
        public Category()
        {
         Name = string.Empty; // Default value for Name
         UserTasks = new List<UserTask>(); // Initialize empty list for UserTasks
        }
    
    }
}
