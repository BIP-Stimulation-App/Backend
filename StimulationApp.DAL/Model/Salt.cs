using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model
{
    public class Salt
    {
        [Key]
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }
        [Required, Column(TypeName = "VARBINARY(MAX)")]
        public byte[] PasswordSalt { get; set; }
    }
}
