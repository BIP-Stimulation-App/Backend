using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.DAL.Model;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace StimulationAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginController(IConfiguration config, LoginService service, UserService userService)
        {
            _config = config;
            _loginService = service;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var validation = _loginService.Validate(userLogin);
                if (validation is null)
                {
                    return Problem("I don't know how you did this but you manage to cause an impossible error! Cudos to you!");
                }

                if (validation.UserName == "")
                {
                    return NotFound("User not found");
                }

                if (validation.Password == "")
                {
                    return Unauthorized("Password Incorrect");
                }
                var user = _loginService.GetCorrespondingUser(validation)!;
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            catch (SqlException e)
            {
                return Problem(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        [AllowAnonymous, HttpPost, Route("Reset")]
        public IActionResult RequestPasswordReset([FromHeader] string email)
        {
            var result = _loginService.RequestPasswordReset(email.ToLower());
            
            if (result == null) 
                return NotFound();
            var user = _userService.GetUserByMail(result.Email);
            if (user == null) 
                return NotFound();
            return SendMail(user, result.Token) ? Ok(result.ExpirationTime) : Problem("Failed to send email, please try again later");
        }

        [AllowAnonymous, HttpPost, Route("ResetWithToken")]
        public IActionResult RequestPasswordToken([FromHeader] string resetToken)
        {
            var result = _loginService.ResetPassword(resetToken);
            return result is null? NotFound("Token has expired or is incorrect"):Ok(GenerateJSONWebToken(result));
        }

        [HttpPost, Route("ChangePassword/{email}")]
        public IActionResult ChangePassword([FromHeader] string password, [FromRoute] string email)
        {
            var current = _loginService.GetCorrespondingLogin(_userService.GetUserByMail(email));//update password && check empty email
            if (current == null) return Unauthorized("You're not logged in correctly");
            current.Password = password;
            var result = _loginService.ChangePassword(current);
            if (result == null) return Problem("Something went wrong, please try again");
            return Ok();
        }

        [HttpPost, Route("Add")]
        public IActionResult AddUser([FromBody] NewAccount account)
        {

            if (!_userService.UsernameInUse(account.UserName))
            {
                return Problem("Username is already in use.");
            }

            if (account.Password == string.Empty)
            {
                BadRequest("Password can not be empty.");
            }

            if (account.FirstName == string.Empty || account.LastName == string.Empty)
            {
                BadRequest("Name can not be empty.");
            }
            if (account.Email is null || !account.Email.Contains('@'))
            {
                return BadRequest("Email is invalid");
            }
            var user = new User
            {
                Role = "user",
                UserName = account.UserName,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email.ToLower()

            };
            var login = new UserLogin
            {
                UserName = account.UserName,
                Password = account.Password
            };

            try
            {
                _userService.AddUser(user);
                _loginService.AddLogin(login);
                GenerateJSONWebToken(user);
                
                return Ok(GenerateJSONWebToken(user));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials

            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        
        private bool SendMail(User user, string resetToken)
        {
            try
            {
                var email = new MimeMessage();
                var adress = EmailConfiguration.Email;
                email.From.Add(new MailboxAddress(EmailConfiguration.Name, EmailConfiguration.Email));
                email.To.Add(new MailboxAddress(user.FirstName, user.Email));
                email.Subject = "Password reset token";

                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $@"<b>Hello {user.FirstName}.</b>

You've just requested a code for your password reset. Your Code is : {resetToken}.

If you did not request this code, please ignore this email.

Thank you."
                };

                using var smtp = new SmtpClient();
                smtp.Connect(EmailConfiguration.SmtpServer, EmailConfiguration.SmtpPort, EmailConfiguration.SmtpSSL);
                smtp.Authenticate(EmailConfiguration.Email, EmailConfiguration.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }



    }
}
