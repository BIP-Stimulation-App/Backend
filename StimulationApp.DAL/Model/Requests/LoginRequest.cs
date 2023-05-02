using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model.Requests
{
    public class LoginRequest
    {
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; }
        [Required, Column(TypeName = "varchar(250)")]
        public string Password { get; set; }
    }
}
