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
    /// Окно для добавления расписания занятий секции.
    /// Позволяет установить время начала и окончания занятий.
    /// </summary>
    public partial class AddScheduleWindow : Window
    {
        private AppDbContext _context;
        private int _sectionId;

        /// <summary>
        /// Инициализирует новый экземпляр окна добавления расписания.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="sectionId">Идентификатор секции, для которой создается расписание</param>
        public AddScheduleWindow(AppDbContext context, int sectionId)
        {
            InitializeComponent();
            _context = context;
            _sectionId = sectionId;
        }

        /// <summary>
        /// Обработчик закрытия окна.
        /// </summary>
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик добавления расписания.
        /// Создает новое расписание для секции с указанными параметрами.
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate == null || EndDate == null)
                return;

            if (string.IsNullOrEmpty(BeginTimeTextBox.Text) || string.IsNullOrEmpty(EndingTimeTextBox.Text))
            {
                MessageBox.Show("Напишите время окончания и начала секции!");
                return;
            }
            

            var section = _context.Sections.FirstOrDefault(s => s.Id == _sectionId);

            if (section == null)
                return;


            ScheduleEntity scheduleEntity = new ScheduleEntity()
            {
                StartTime = (DateTime)StartDate.SelectedDate,
                EndTime = (DateTime)EndDate.SelectedDate,
                BeginTime = BeginTimeTextBox.Text,
                EndingTime = EndingTimeTextBox.Text,
                Section = section

            };

            _context.Schedules.Add(scheduleEntity);
            _context.SaveChanges();

            section.Schedule = scheduleEntity;

            _context.SaveChanges();

            MessageBox.Show("Расписание добавлено!");
            this.Close();
        }
    }
}
