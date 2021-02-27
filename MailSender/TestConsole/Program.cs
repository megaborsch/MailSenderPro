using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Формирование письма
            MailMessage mm = new MailMessage("me@yandex.ru", "получатель@yandex.ru");
            mm.Subject = "Заголовок письма";
            mm.Body = "Содержимое письма";
            mm.IsBodyHtml = false; // Не используем html в теле письма

            // Авторизуемся на smtp-сервере и отправляем письмо
            SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25);
            sc.EnableSsl = true;
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential("отправитель@yandex.ru", "password");
            sc.Timeout = 1000;
            try
            {
                sc.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to send message " + ex.ToString());
            }

        }
    }
}
