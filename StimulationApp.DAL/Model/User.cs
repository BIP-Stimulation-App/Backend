using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StimulationAppAPI.DAL.Model.Requests;

namespace StimulationAppAPI.DAL.Model
{
    [Table("User")]
    public class User
    {
        public User(){
            Medications ??= new List<Medication>();
            Login ??= new UserLogin();
        }

        [Key]
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; } = null!;

        [Required, Column(TypeName = "varchar(30)")]
        public string FirstName { get; set; } = null!;

        [Required, Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress, Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = null!;

        [Required, Column(TypeName = "varchar(25)")]
        public string Role { get; set; } = null!;

        public ICollection<Medication> Medications{ get; set; }
        public UserLogin Login { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is User user)
            {
                return UserName == user.UserName;
            }

            return false;

        }


    }
}
