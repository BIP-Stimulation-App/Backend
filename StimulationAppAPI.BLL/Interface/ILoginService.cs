using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.DAL.Model.Requests;

namespace StimulationAppAPI.BLL.Interface
{
    public interface ILoginService
    {
        public UserLogin AddLogin(UserLogin login);
        public UserLogin? Validate(LoginRequest login);
        public User? GetCorrespondingUser(UserLogin login);
        public UserLogin? GetCorrespondingLogin(User login);
        public PasswordReset? RequestPasswordReset(string email);
        public User? ResetPassword(string token);
        public UserLogin? ChangePassword(UserLogin login);
    };
}
