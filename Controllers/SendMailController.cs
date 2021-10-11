using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Route("api/sendMail")]
    public class SendMailController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public SendMailController(ApplicationContext context)
        {
            _context = context;
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
        [HttpPut]
        public async Task<IActionResult> GetAsync([FromBody] SendGmail sendGmail)
        {
            var message = await SendGmail(sendGmail.From, sendGmail.To, sendGmail.Subject, sendGmail.Body, sendGmail.Gmail, sendGmail.Password);
            return Ok(message);
        }
    }
}