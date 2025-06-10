using SportSectionWPF2.Data;
using SportSectionWPF2.Entities;
using SportSectionWPF2.View.Windows;
using SportSectionWPF2.View.Windows.WindowForAdminView;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SportSectionWPF2
{
    /// <summary>
    /// Главное окно приложения.
    /// Предоставляет функционал авторизации пользователей различных ролей.
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _сontext;

        /// <summary>
        /// Инициализирует новый экземпляр главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _сontext = new AppDbContext();
        }

        /// <summary>
        /// Обработчик нажатия кнопки входа.
        /// Проверяет учетные данные и открывает соответствующее окно в зависимости от роли пользователя.
        /// </summary>
        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text == "Admin" && PasswordTextBox.Password == "Admin")
            {
                AdminViewWindow adminViewWindow = new AdminViewWindow(_сontext);
                MessageBox.Show("Вошли от имени администратора");
                adminViewWindow.Show();

                return;
            }

            string username = UserNameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            var user = _сontext.Users.FirstOrDefault(u => u.Name == username && u.Password == password);

            if (user != null)
            {
                var isAdmin = _сontext.Admins.FirstOrDefault(a => a.UserId == user.Id);
                var isCoach = _сontext.Coachs.FirstOrDefault(c => c.UserId == user.Id);
                var isMember = _сontext.Members.FirstOrDefault(m => m.UserId == user.Id);

                if (isAdmin != null)
                {
                    var adminViewWindow = new AdminViewWindow(_сontext);
                    MessageBox.Show($"Вошли от имени {user.Name} (Администратор)");
                    adminViewWindow.Show();
                }
                else if (isCoach != null)
                {
                    var coachViewWindow = new CoachWindow(_сontext, isCoach.Id);
                    MessageBox.Show($"Вошли от имени {user.Name} (Тренер)");
                    coachViewWindow.Show();
                }
                else if (isMember != null)
                {
                    var memberViewWindow = new MemberWindow(_сontext, isMember.Id);
                    MessageBox.Show($"Вошли от имени {user.Name} (Участник)");
                    memberViewWindow.Show();
                }
                else
                {
                    MessageBox.Show("Пользователь не полностью зарегистрирован.");
                    return;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        // Закомментированные методы оставлены без документации
    }
}