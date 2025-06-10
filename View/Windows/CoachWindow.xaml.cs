using SportSectionWPF2.Data;
using SportSectionWPF2.Entities;
using SportSectionWPF2.View.Windows.WindowForAdminView;
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

namespace SportSectionWPF2.View.Windows
{
    /// <summary>
    /// Окно тренера.
    /// Предоставляет функционал для управления секциями и отметки посещаемости участников.
    /// </summary>
    public partial class CoachWindow : Window
    {
        private AppDbContext _context;
        private int _coachId;

        /// <summary>
        /// Инициализирует новый экземпляр окна тренера.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="coachId">Идентификатор тренера</param>
        public CoachWindow(AppDbContext context, int coachId)
        {
            InitializeComponent();
            _coachId = coachId;
            _context = context;
            DbLoad();
        }

        /// <summary>
        /// Загружает данные из базы данных и обновляет интерфейс.
        /// </summary>
        private void DbLoad()
        {
            _context.Sections.Include(s => s.Coach).Load();

            SectionsList.ItemsSource = _context.Sections.Local
                .Where(s => s.Coach != null && s.Coach.Id == _coachId)
                .ToList();
        }

        /// <summary>
        /// Обработчик выбора секции в списке.
        /// Обновляет информацию о выбранной секции и её участниках.
        /// </summary>
        private void SectionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSection = SectionsList.SelectedItem as SectionEntity;
            if (selectedSection == null)
                return;

            var section = _context.Sections
                .Include(s => s.Coach)
                .Include(s => s.Schedule)
                .Include(s => s.Members)
                .FirstOrDefault(s => s.Id == selectedSection.Id);

            if (section?.Schedule != null)
            {
                SchedualLabel.Content = $"Начало: {section.Schedule.StartTime:dd.MM.yyyy}, конец: {section.Schedule.EndTime:dd.MM.yyyy} с {section.Schedule.BeginTime} до {section.Schedule.EndingTime}";
            }
            else
            {
                SchedualLabel.Content = "Расписание отсутствует";
            }

            var sections = _context.Sections
                .Include(s => s.Members.Select(m => m.User))  // загружаем участников и их User
                .Where(s => s.CoachId == _coachId)
                .ToList();

            var members = sections
                .SelectMany(s => s.Members)
                .Distinct()
                .ToList();

            MembersDataGrid.ItemsSource = members;
        }

        /// <summary>
        /// Обработчик отметки посещаемости участника.
        /// </summary>
        private void CheckMarkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_coachId == 0)
            {
                MessageBox.Show("Ошибка авторизации тренера!");
                return;
            }

            var selectedMember = MembersDataGrid.SelectedItem as MemberEntity;
            if (selectedMember == null)
            {
                MessageBox.Show("Выберите участника!");
                return;
            }

            var attendance = new AttendancesEntity
            {
                Date = DateTime.Now,
                IsPresent = true
            };

            if (selectedMember.Attendances == null)
                selectedMember.Attendances = new List<AttendancesEntity>();

            selectedMember.Attendances.Add(attendance);

            _context.Attendances.Add(attendance);

            _context.SaveChanges();

            MembersDataGrid.Items.Refresh();

            MessageBox.Show("Отметка поставлена!");
        }

        /// <summary>
        /// Обработчик удаления расписания секции.
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsList.SelectedItem == null)
            {
                MessageBox.Show("Выберите секцию!");
                return;
            }

            var selectedSection = SectionsList.SelectedItem as SectionEntity;

            if (selectedSection == null)
                return;

            var SectionForDelte = _context.Sections.FirstOrDefault(s => s.Id == selectedSection.Id);

            if (SectionForDelte.Schedule == null)
            {
                MessageBox.Show("Расписания нет в секции!");
                return;
            }

            var deletedSchedule = _context.Schedules.FirstOrDefault(s => s.Id == SectionForDelte.Schedule.Id);

            _context.Schedules.Remove(deletedSchedule);
            _context.SaveChanges();

            MessageBox.Show("Расписание удалено!");
        }

        /// <summary>
        /// Обработчик добавления расписания для секции.
        /// </summary>
        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSection = (SectionsList.SelectedItem as SectionEntity);

            if (selectedSection == null)
                return;

            AddScheduleWindow addScheduleWindow = new AddScheduleWindow(_context, selectedSection.Id);
            addScheduleWindow.ShowDialog();

            SectionsList_SelectionChanged(null, null);
        }
    }
}
