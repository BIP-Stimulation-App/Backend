using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model.Requests
{
    internal class UpdateUser
    {
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; } = null!;

        [Required, Column(TypeName = "varchar(30)")]
        public string FirstName { get; set; } = null!;

        [Required, Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress, Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = null!;
    }
}
