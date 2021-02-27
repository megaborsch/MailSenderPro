using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public static class TestData
    {
        public static IList<Server> Servers { get; } = new List<Server>
        {
        new Server
             {
             Id = 1,
             Name = "Яндекс!",
             Address = "smpt.yandex.ru",
             Port = 465,
             UseSSL = true,
             Login = "user@yandex.ru",
             Password = "PassWord",
             },
        new Server
             {
             Id = 2,
             Name = "gMail",
             Address = "smpt.gmail.com",
             Port = 465,
             UseSSL = true,
             Login = "user@yandex.ru",
             Password = "PassWord",
             },
        };

        public static IList<Sender> Senders { get; } = new List<Sender>
        {
        new Sender
             {
             Id = 1,
             Name = "Иванов",
             Address = "ivanov@server.ru",
             Description = "Почта от Иванова"
             },
        new Sender
             {
             Id = 2,
             Name = "Петров",
             Address = "petrov@server.ru",
             Description = "Почта от Петрова"
             },
        new Sender
             {
             Id = 3,
             Name = "Сидоров",
             Address = "sidorov@server.ru",
             Description = "Почта от Сидорова"
             },
        };
        public static IList<Recipient> Recipients { get; } = new List<Recipient>
         {
         new Recipient
             {
             Id = 1,
             Name = "Иванов",
             Address = "ivanov@server.ru",
             Description = "Почта для Иванова"
             },
         new Recipient
             {
             Id = 2,
             Name = "Петров",
             Address = "petrov@server.ru",
             Description = "Почта для Петрова"
             },
         new Recipient
             {
             Id = 3,
             Name = "Сидоров",
             Address = "sidorov@server.ru",
             Description = "Почта для Сидорова"
             },
         };
        public static IList<Message> Messages { get; } = Enumerable
         .Range(1, 10)
         .Select(i => new Message
         {
             Id = i,
             Tittle = $"Сообщение {i}",
             Body = $"Текст сообщения {i}"
         })
         .ToList();
    }
}
