using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace todolistReactAsp.MailUtills
{
    public class MailUtills
    {
        public static async Task<string> SendMail(string _from, string _to, string _subject, string _body)
        {
            MailMessage message = new MailMessage(_from, _to, _subject, _body);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);
            using var smtpClient = new SmtpClient("localhost");
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
    }
}