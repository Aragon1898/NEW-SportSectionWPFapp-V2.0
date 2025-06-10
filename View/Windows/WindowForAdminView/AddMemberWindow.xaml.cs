using SportSectionWPF2.Data;
using SportSectionWPF2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace SportSectionWPF2.View.Windows.WindowForAdminView
{
    /// <summary>
    /// Окно для добавления нового участника в секцию.
    /// Позволяет создать нового пользователя или выбрать существующего участника.
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        private AppDbContext _context;
        private int _sectionId;

        /// <summary>
        /// Инициализирует новый экземпляр окна добавления участника.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="sectionId">Идентификатор секции, в которую добавляется участник</param>
        public AddMemberWindow(AppDbContext context, int sectionId)
        {
            InitializeComponent();
            _context = context;
            _sectionId = sectionId;
            DbLoad();
        }

        /// <summary>
        /// Обработчик закрытия окна.
        /// </summary>
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Загружает данные из базы данных и обновляет список участников.
        /// </summary>
        private void DbLoad()
        {
            if (_context.Members != null)
            {
                _context.Members.Load();
                AllMembersListBox.ItemsSource = _context.Members.Local.ToList();
                AllMembersListBox.DisplayMemberPath = "DisplayName";
            }
        }

        /// <summary>
        /// Обработчик добавления нового участника.
        /// Создает нового пользователя и участника в базе данных.
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите имя участника!");
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите пароль для добавления!");
                return;
            }
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Введите E-mail для добавления участника!");
                return;
            }

            UserEntity userEntity = new UserEntity()
            {
                Email = EmailTextBox.Text,
                Name = NameTextBox.Text,    
                Password = PasswordTextBox.Text,
                Role = "Участник",
                
                
            };


            _context.Users.Add(userEntity);
            _context.SaveChanges();

            MemberEntity member = new MemberEntity()
            {
                User = userEntity,
                Sections = new List<SectionEntity>(),
                Achievements = new List<AchievementEntity>()
                
            };


            var selectedSection = _context.Sections.FirstOrDefault(s => s.Id == _sectionId);

            member.Sections.Add(selectedSection);



            _context.Members.Add(member);
            _context.SaveChanges();


            var ach = _context.Members.FirstOrDefault(s => s.Id == member.Id);
            var a = ach.Achievements;

            MessageBox.Show("Участник добавлен!");

            this.Close();
        }

        /// <summary>
        /// Обработчик выбора существующего участника.
        /// Добавляет выбранного участника в текущую секцию.
        /// </summary>
        private void ChooseExistedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMember = AllMembersListBox.SelectedItem as MemberEntity;

            if (selectedMember == null)
                return;

            var selectedSection = _context.Sections.FirstOrDefault(s => s.Id == _sectionId);

            selectedMember.Sections.Add(selectedSection);

            _context.Members.Add(selectedMember);
            _context.SaveChanges();

            MessageBox.Show("Участник добавлен!");

            this.Close();
        }
    }
}
