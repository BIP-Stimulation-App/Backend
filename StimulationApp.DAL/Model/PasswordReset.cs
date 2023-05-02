using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model
{
    public class PasswordReset
    {
        [Key]
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }
        [Required,DataType("DATETIME")]
        public DateTime ExpirationTime { get; set; }
        [Required, DataType("varchar(50)")]
        public string Token { get; set; }
        [Required]
        public UserLogin UserLogin { get; set; }

    }
}
