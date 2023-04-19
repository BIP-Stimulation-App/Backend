using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StimulationAppAPI.DAL.Model
{
    [Table("Login")]
    public class UserLogin
    {
        [Key]
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }
        [Required, Column(TypeName = "varchar(250)")]
        public string Password { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            try
            {
                var login = (UserLogin)obj;
                return login.UserName == UserName && login.Password == Password;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
