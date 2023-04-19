using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StimulationAppAPI.DAL.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }
        
        [Required, Column(TypeName = "varchar(30)")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        
        [Required, EmailAddress, Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        [Required, Column(TypeName = "varchar(25)")]
        public string Role { get; set; }
    }
}
