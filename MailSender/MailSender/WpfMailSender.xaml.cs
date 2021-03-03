using MailSender.Data;
using MailSender.Infrastructure.Services;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
            //cbSenderSelect.ItemsSource = VariablesClass.Senders;
            //cbSenderSelect.DisplayMemberPath = "Key";
            //cbSenderSelect.SelectedValuePath = "Value";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSendAtOnce_Click(object Sender, RoutedEventArgs e)
        {
            // Извлекаем исходные параметры по возможности
            if (!(cbSenderSelect.SelectedItem is Sender sender)) return;
            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(cbSmtpSelect.SelectedItem is Server server)) return;
            if (!(MessagesList.SelectedItem is Message message)) return;
            // Если одни из параметров невозможно получить, то выходим
            // Создаём объект-рассыльщик и заполняем параметры сервера
            var mail_sender = new SmtpSender(
            server.Address, server.Port, server.UseSSL,
            server.Login, server.Password);
            // При отправке почты может возникнуть проблема. Ставим перехват исключения.
            try
            {
                // Запускаем таймер
                var timer = Stopwatch.StartNew();
                // И запускаем процесс отправки почты
                mail_sender.Send(
                sender.Address, recipient.Address,
                message.Tittle, message.Body);
                timer.Stop(); // По завершении останавливаем таймер
                              // Если почта успешно отправлена, то отображаем диалоговое окно
                MessageBox.Show(
                $"Почта успешно отправлена за {timer.Elapsed.TotalSeconds:0.##}c",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            // Если случилась ошибка, то перехватываем исключение
            catch (SmtpException) // Перехватывает строго нужное нам исключение!
            {
                MessageBox.Show(
                "Ошибка при отправке почты",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }
        /// <summary>Обработчик события кнопки создания нового сервера</summary>
        private void OnAddServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!ServerEditDialog.Create(
            out var name,
            out var address,
            out var port,
            out var ssl,
            out var description,
            out var login,
            out var password))
                return;
            var server = new Server
            {
                Id = (TestData.Servers.Count>0) ? TestData.Servers.DefaultIfEmpty().Max(s => s.Id):0 + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };
            TestData.Servers.Add(server);
            cbSmtpSelect.ItemsSource = null;
            cbSmtpSelect.ItemsSource = TestData.Servers;
            cbSmtpSelect.SelectedItem = server;
        }

        /// <summary>Обработчик события кнопки редактирования сервера</summary>
        private void OnEditServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(cbSmtpSelect.SelectedItem is Server server)) return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditDialog.ShowDialog("Редактирование сервера",
            ref name,
            ref address, ref port, ref ssl,
            ref description,
            ref login, ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;
            cbSmtpSelect.ItemsSource = null;
            cbSmtpSelect.ItemsSource = TestData.Servers;
            cbSmtpSelect.SelectedItem = server;
        }
        private void OnDeleteServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(cbSmtpSelect.SelectedItem is Server server)) return;
            TestData.Servers.Remove(server);
            cbSmtpSelect.ItemsSource = null;
            cbSmtpSelect.ItemsSource = TestData.Servers;
            cbSmtpSelect.SelectedItem = TestData.Servers.FirstOrDefault();
        }
    }
}