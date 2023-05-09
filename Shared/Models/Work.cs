using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using todo_blazor_auth.Shared.Models;

namespace todo_blazor_auth.Shared.Models
{
    public class Work
    {
        [Required]
        public string Name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /*
        *   User reference
        */
        public int UserId { get; set; }

        public string Email { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}