using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Context;
using StimulationAppAPI.DAL.Model;

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
            var salted = new Salt();
            HashPassword(login, salted);
            _context.Logins.Add(login);
            _context.salts.Add(salted);
            _context.SaveChanges();
            return login;
        }
        
        public UserLogin? Validate(UserLogin login)
        {
            var creds = _context.Logins.Find(login.UserName);
            if (creds is null)
            {
                login.UserName = "";
                login.Password = "";
                return login;
            }
            
            var salt = _context.salts.Find(creds.UserName)!;
            var correct = VerifyPassword(login.Password, creds.Password, salt.PasswordSalt);

            if (!correct)
            {
                login.Password = "";
            }
            return correct  ? creds : login;
        }
        
        public UserLogin? Delete(UserLogin login)
        {
            var correctLogin = Validate(login);
            if (correctLogin is null) return null;

            _context.Logins.Remove(correctLogin);
            _context.SaveChanges();
            return correctLogin;
        }

        public UserLogin? Update(UserLogin login)
        {
            if (Validate(login) is null) return null;
            _context.Logins.Update(login);
            return login;
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
            var result = GetUserLogin(login.UserName);
            if (result is null) return null;
            result.Password = login.Password;
            try
            {
                var salted = _context.salts.Find(login.UserName)!;
                HashPassword(result, salted);
                _context.Logins.Update(login);
                _context.salts.Update(salted);
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
            var result = _context.PasswordResets.FirstOrDefault(reset => reset.Email == email);
            if (result != default)
            {
                if (DateTime.Compare(result.ExpirationTime, DateTime.Now) < 0)
                {
                    _context.PasswordResets.Remove(result);
                    var newReset = new PasswordReset()
                    {
                        Email = email,
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
                Email = email,
                ExpirationTime = DateTime.Now.AddMinutes(15),
                Token = GenerateRandomToken()
            };
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
                    _context.PasswordResets.Remove(result);
                    _context.SaveChanges();
                    return _context.Users.FirstOrDefault(user => user.Email == result.Email);
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
                int type = rng.Next(1, 4);
                switch (type)
                {
                    //number
                    case 1:
                        token += rng.Next(0,10);
                        continue;
                    case 2://lowercase
                        token += (char)('a' + rng.Next(0, 26));
                        continue;
                    default:
                        token += (char)('A' + rng.Next(0, 26));

                        continue;
                }
            }

            return token;
        }
        
        const int KeySize = 64;
        const int Iterations = 350000;
        readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
        private void HashPassword(UserLogin login, Salt salted)
        {
            if (salted.PasswordSalt is null || salted.PasswordSalt.Length == 0)
            {
                salted.PasswordSalt = RandomNumberGenerator.GetBytes(KeySize);
                salted.UserName = login.UserName;
            }
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(login.Password),
                salted.PasswordSalt,
                Iterations,
                _hashAlgorithm,
                KeySize);
            login.Password = Convert.ToHexString(hash);
        }
        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
