using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Infrastructure.Services
{
    public class SmtpSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;
        public SmtpSender(
        string Address, int Port, bool UseSSL,
        string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _UseSsl = UseSSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(
        string From, string To,
        string Title, string Message)
        {
            using var message = new MailMessage(From, To)
            {
                Subject = Title,
                Body = Message
            };
            using var client = new SmtpClient(_Address, _Port)
            {
                EnableSsl = _UseSsl,
                Credentials = new NetworkCredential(_Login, _Password)
            };
            //client.Send(message);
        }
    }
}
