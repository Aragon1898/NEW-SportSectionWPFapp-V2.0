using SportSectionWPF2.Data;
using SportSectionWPF2.Entities;
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
using System.Windows.Shapes;

namespace SportSectionWPF2.View.Windows.WindowForAdminView
{
    /// <summary>
    /// Окно для создания новой спортивной секции.
    /// Позволяет задать название новой секции.
    /// </summary>
    public partial class AddSectionWindow : Window
    {
        private AppDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр окна создания секции.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public AddSectionWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        /// <summary>
        /// Обработчик добавления новой секции.
        /// Создает новую секцию с указанным названием.
        /// </summary>
        private void AddSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SectionNameTextBox.Text))
            {
                MessageBox.Show("Введите название!");
                return;
            }

            SectionEntity entity = new SectionEntity()
            {
                Name = SectionNameTextBox.Text,
            };

            _context.Sections.Add(entity);
            _context.SaveChanges();

            MessageBox.Show("Секция создана!");
            this.Close();
        }

        /// <summary>
        /// Обработчик закрытия окна.
        /// </summary>
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
