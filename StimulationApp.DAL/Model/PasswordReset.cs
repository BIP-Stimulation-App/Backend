using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model
{
    public class PasswordReset
    {
        
        [Key,Required,EmailAddress, DataType("nchar(100)")]
        public string Email { get; set; }

        [Required,DataType("DATETIME")]
        public DateTime ExpirationTime { get; set; }
        [Required, DataType("varchar(50)")]
        public string Token { get; set; }
        
    }
}
