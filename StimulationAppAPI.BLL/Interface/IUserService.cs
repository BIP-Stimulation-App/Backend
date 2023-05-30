using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.BLL.Interface
{
    public interface IUserService
    {
        public User? GetUser(string username);
        public User? GetUserByMail(string email);
        public bool UsernameInUse(string username);
        public List<User>? GetUsers();
        public User? Delete(string username);
        public User? AddUser(User user);
        public User? UpdateUser(User user);
        public User? UpdateEmail(string username, string email);
        public User? UpdateUserName(string oldUsername, string newUsername);
        public User? UpdatePrivacyMode(string username, bool anonymous);


    }
}
