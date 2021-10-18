using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;
namespace todolistReactAsp.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly JWTSettings _jwtsettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserController(
            ApplicationContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<JWTSettings> jwtsettings
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtsettings = jwtsettings.Value;
        }
        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.User.AsQueryable();
            return Ok(query);
        }
        [HttpGet("checkUser")]
        public IActionResult CheckUser()
        {
            return Ok("Success");
        }
        public static async Task<string> SendGmail(string _from, string _to, string _subject, string _body, string _gmail, string _password)
        {
            MailMessage message = new MailMessage(_from, _to, _subject, _body);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);
            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_gmail, _password);
            try
            {
                await smtpClient.SendMailAsync(message);
                return "Success";
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] User user)
        {
            var _user = new User { UserName = user.Email, Name = user.Name, Email = user.Email };
            var result = await _userManager.CreateAsync(_user, user.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
                // var link = Url.Action("VerifyEmail", new { userId = _user.Id, code });
                // var callbackUrl = Url.Page(
                //     "ConfirmEmail",
                //     pageHandler: null,
                //     values: new { userId = user.Id, code = code },
                //     protocol: Request.Scheme);
                await SendGmail
                (
                    "vankienars98@gmail.com",
                    user.Email,
                    "Email xác nhận!",
                     $"Đây là email xác nhận đăng ký tài khoản của bạn vui lòng bấm vào đây <a href='http://localhost:5000/verifyEmail/?code={_user.Id}?code={code}'>Truy cập website</a>.",
                    "vankienars98@gmail.com",
                    "vankienars98"
                    );
                return Ok("Success");
            }
            else
            {
                return Ok(result.Errors);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] User user)
        {
            var _user = await _context.User.FirstOrDefaultAsync(n => n.Email == user.Email);
            var result = await _signInManager.PasswordSignInAsync
            (
                user.Email,
                user.Password,
                true,
                lockoutOnFailure: true
            );
            if (result.Succeeded)
            {
                UserWithToken userWithToken = new UserWithToken(_user);
                userWithToken.AccessToken = GenerateAccessToken(_user.Id);
                return Ok(userWithToken);
            }
            if (result.IsLockedOut)
            {
                return Ok("Lock");
            }
            else
            {
                return Ok("Invalid");
            }
            // var _user = await _context.User.FirstOrDefaultAsync(n => n.UserName == user.UserName && n.Password == user.Password);
            // if (_user != null)
            // {
            //     UserWithToken userWithToken = new UserWithToken(_user);
            //     userWithToken.AccessToken = GenerateAccessToken(_user.Id);
            //     return Ok(userWithToken);
            // }
            // else
            // {
            //     return Ok("Error");
            // }
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Success");
        }
        [AllowAnonymous]
        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] GetCodeEmail getCodeEmail)
        {
            var _user = await _userManager.FindByIdAsync(getCodeEmail.userFe);
            if (_user == null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(_user, getCodeEmail.codeFe);
            if (result.Succeeded)
            {
                return Ok("VerifySuccess");
            }
            else
            {
                return Ok("Error");
            }
        }

    }
}