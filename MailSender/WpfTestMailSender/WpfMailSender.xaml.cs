using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }
        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            List<string> listStrMails = new List<string> { "fearmantrue@yandex.ru" };  // Список email'ов //кому мы отправляем письмо
            string strPassword = passwordBox.Password;  // для WinForms - string strPassword = passwordBox.Text;
            foreach (string mail in listStrMails)
            {
                // Используем using, чтобы гарантированно удалить объект MailMessage после использования
                using (MailMessage mm = new MailMessage("fearmantrue@yandex.ru", mail))
                {
                    // Формируем письмо
                    mm.Subject = "Привет из C#"; // Заголовок письма
                    mm.Body = "Hello, world!";       // Тело письма
                    mm.IsBodyHtml = false;           // Не используем html в теле письма
                                                     // Авторизуемся на smtp-сервере и отправляем письмо
                                                     // Оператор using гарантирует вызов метода Dispose, даже если при вызове 
                                                     // методов в объекте происходит исключение.
                    using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 465))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential("fearmantrue@yandex.ru", strPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                        }
                    }
                } 
            }
            MessageBox.Show("Работа завершена.");
        }

    }
}
