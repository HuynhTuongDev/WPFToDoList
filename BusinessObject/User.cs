using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
        public class User
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }

            [Required]
            [StringLength(100)]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(100)]
            public string Password { get; set; } = null!;

            [Required]
            [StringLength(100)]
            public string FullName { get; set; } = null!;

            [Required]
            public int Role { get; set; }


            public ICollection<Todo> Todos { get; set; } // Danh sách các công việc
        }
}
