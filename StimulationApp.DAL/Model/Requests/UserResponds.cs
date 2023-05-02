using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model.Requests
{
    public class UserResponds
    {
        public UserResponds(User user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
