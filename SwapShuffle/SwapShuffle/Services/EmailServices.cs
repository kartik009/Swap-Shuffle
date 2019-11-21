using SwapShuffle.Helper;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SwapShuffle.Services
{
    public class EmailServices
    {
        MailMessage mail;
        SmtpClient SmtpServer;
        public bool SendVerifyCode(long sender,string Subject,string Message)
        {
            try
            {

                mail = new MailMessage();
                SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(EmailSettings.email);
                mail.To.Add(sender.ToString() + "@daiict.ac.in");
                mail.Subject = Subject;
                mail.Body = Message;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailSettings.email, EmailSettings.pass);

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
