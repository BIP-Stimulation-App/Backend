using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Context;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.DAL.Model.Requests;

namespace StimulationAppAPI.BLL.Service
{
    public class LoginService:ILoginService
    {
        private readonly UserContext _context;

        public LoginService(UserContext context)
        {
            _context = context;
        }
        public UserLogin AddLogin(UserLogin login)
        {
            HashPassword(login);
            return login;
        }
        
        public UserLogin Validate(LoginRequest login)
        {
            
            var creds = _context.Logins.FirstOrDefault(l => l.UserName == login.UserName);
            if (creds is null)
            {
                Console.WriteLine($"username \"{login.UserName}\n was not found");
                return new UserLogin
                {
                    Password = "",
                    UserName = ""
                };
            }
            
            var salt = _context.Salts.First(s => s.UserName == creds.UserName);
            var correct = VerifyPassword(login.Password, creds.Password, salt.PasswordSalt);

            if (!correct)
            {
                login.Password = "";
                Console.WriteLine("Password is incorrect");
            }
            return creds;
        }

        public User? GetCorrespondingUser(UserLogin login)
        {
            return _context.Users.Find(login.UserName);
        }

        public UserLogin? GetCorrespondingLogin(User login)
        {
            return _context.Logins.Find(login.UserName);
        }

        public UserLogin? GetUserLogin(string userName)
        {
            return _context.Logins.Find(userName);
        }

        public UserLogin? ChangePassword(UserLogin login)
        {
            var result = GetUserLogin(login.User.UserName);
            if (result is null) return null;
            result.Password = login.Password;
            try
            {
                var salted = _context.Salts.Find(login.User.UserName)!;
                HashPassword(result, salted);
                _context.Logins.Update(login);
                _context.SaveChanges();

                return result;
            }
            catch
            {
                return null;
            }
        }

        public PasswordReset? RequestPasswordReset(string email)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == email);
            if (user == default) return null;
            var login = _context.Logins.FirstOrDefault(login => login.UserName == user.UserName);
            var result = _context.PasswordResets.FirstOrDefault(reset => reset.UserLogin.User.Email == email);
            if (result != default)
            {
                if (DateTime.Compare(result.ExpirationTime, DateTime.Now) < 0)
                {
                    _context.PasswordResets.Remove(result);
                    var newReset = new PasswordReset()
                    {
                        UserLogin = user.Login,
                        ExpirationTime = DateTime.Now.AddMinutes(15),
                        Token = GenerateRandomToken()
                    };
                    _context.PasswordResets.Add(newReset);
                    _context.SaveChanges();
                    return newReset;
                }
                return result;
                
            }
            var reset = new PasswordReset()
            {
                UserLogin = user.Login,
                ExpirationTime = DateTime.Now.AddMinutes(15),
                Token = GenerateRandomToken(),
                UserName = user.UserName
            };
            
            Console.WriteLine(reset);
            _context.PasswordResets.Add(reset);
            _context.SaveChanges();
            return reset;
            
        }

        public User? ResetPassword(string token)
        {
            var result = _context.PasswordResets.FirstOrDefault(reset => reset.Token == token);
           
            if (result != default)
            {
                if (DateTime.Compare(result.ExpirationTime, DateTime.Now) > 0)
                {
                    var userAcc=_context.Users.FirstOrDefault(user => user.UserName == result.UserName);
                    _context.PasswordResets.Remove(result);
                    _context.SaveChanges();
                    return _context.Users.FirstOrDefault(user => user.Email == userAcc.Email);
                }
            }
            return null;
        }

        public string GenerateRandomToken()
        {
            string token = "";
            Random rng = new Random();

            for (int i = 0; i < 5; i++)
            {
                token += rng.Next(0, 10);
            }

            return token;
        }
        
        const int KeySize = 64;
        const int Iterations = 350000;
        readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
        private void HashPassword(UserLogin login, Salt? salt = null)
        {
            login.Salt = salt;
            if (login.Salt is null || login.Salt.PasswordSalt.Length == 0)
            {
                login.Salt = new Salt
                {
                    PasswordSalt = RandomNumberGenerator.GetBytes(KeySize),
                    UserName = login.User.UserName
                };
            }
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(login.Password),
                login.Salt.PasswordSalt,
                Iterations,
                _hashAlgorithm,
                KeySize);
            login.Password = Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
