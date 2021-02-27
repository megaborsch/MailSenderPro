using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Body { get; set; }
    }
}
