using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Model;
using StimulationAppAPI.DAL.Context;
using System.IO.Pipes;

namespace StimulationAppAPI.BLL.Service
{
    public class UserService:IUserService
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }

        public User? GetUser(string firstname, string lastname)
        {
            return _context.Users.First(user => user.FirstName == firstname && user.LastName == lastname);
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == username);
        }

        public User? GetUserByMail(string email)
        {
            return _context.Users.First(user => user.Email == email);
        }

        public bool UsernameInUse(string username)
        {
            return GetUser(username) == null;
        }

        public List<User>? GetUsers()
        {
            return _context.Users.ToList();
        }

        public User? Delete(string username)
        {
            var user = GetUser(username);
            if (user is null)
            {
                return null;
            }
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public User? AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }
        public User? UpdateUser(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }
        public User? UpdateEmail(string username, string email)
        {
            try
            {
                var user =_context.Users.Find(username);
                if (user is null) return null;
                user.Email = email;
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public User? UpdateUserName(string oldUsername, string newUsername)
        {
            try
            {
                var user = _context.Users.Find(oldUsername);
                if (user is null) return null;
                _context.Users.Remove(user);
                user.UserName = newUsername;
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }
        public User? UpdatePrivacyMode(string username,bool anonymous)
        {
            try
            {
                var user = _context.Users.Find(username);
                if (user is null) return null;
                user.Anonymous = anonymous;
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
