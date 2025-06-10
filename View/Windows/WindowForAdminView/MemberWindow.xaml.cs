using SportSectionWPF2.Data;
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
using System.Data.Entity;
using SportSectionWPF2.Entities;
using System.Runtime.Remoting.Contexts;

namespace SportSectionWPF2.View.Windows.WindowForAdminView
{
    /// <summary>
    /// Окно просмотра информации об участнике.
    /// Отображает секции, в которых состоит участник, и его достижения.
    /// </summary>
    public partial class MemberWindow : Window
    {
        private int _memberId;
        private AppDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр окна информации об участнике.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="memberId">Идентификатор участника</param>
        public MemberWindow(AppDbContext context, int memberId)
        {
            InitializeComponent();
            _context = context;
            _memberId = memberId;
            DbLoad();
        }

        /// <summary>
        /// Загружает данные об участнике из базы данных.
        /// </summary>
        private void DbLoad()
        {
            // Получаем все секции, где есть участник с заданным _memberId
            var sections = _context.Sections
                .Include(s => s.Members)
                .Where(s => s.Members.Any(m => m.Id == _memberId))
                .ToList();
            SectionsList.ItemsSource = sections;

            var archi = _context.Achievements.Where(s => s.Member.Id == _memberId);

            if (archi != null)
            {
                AchivmentDataGrid.ItemsSource = archi.ToList();
            }
        }

        /// <summary>
        /// Обработчик выбора секции в списке.
        /// Обновляет информацию о выбранной секции.
        /// </summary>
        private void SectionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSection = SectionsList.SelectedItem as SectionEntity;
            UpdateSectionInfo(selectedSection, CoachSection, SchedualLabel, _context);
        }

        /// <summary>
        /// Обновляет информацию о секции на форме.
        /// </summary>
        /// <param name="selectedSection">Выбранная секция</param>
        /// <param name="coachLabel">Метка для отображения информации о тренере</param>
        /// <param name="scheduleLabel">Метка для отображения информации о расписании</param>
        /// <param name="context">Контекст базы данных</param>
        public void UpdateSectionInfo(SectionEntity selectedSection, Label coachLabel, Label scheduleLabel, AppDbContext context)
        {
            if (selectedSection == null)
            {
                coachLabel.Content = "";
                scheduleLabel.Content = "";
                return;
            }

            var section = context.Sections
                .Include(s => s.Coach)
                .Include(s => s.Schedule)
                .FirstOrDefault(s => s.Id == selectedSection.Id);

            if (section == null)
            {
                coachLabel.Content = "Тренер: не назначен";
                scheduleLabel.Content = "Расписание: не задано";
                return;
            }

            if (section.Coach != null)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == section.Coach.UserId);
                coachLabel.Content = $"Тренер секции: {user.Name}";
            }
            else
            {
                coachLabel.Content = "Тренер: не назначен";
            }

            if (section.Schedule != null)
            {
                scheduleLabel.Content = $"Расписание: {section.Schedule.StartTime:HH:mm} - {section.Schedule.EndTime:HH:mm} c {section.Schedule.BeginTime} до {section.Schedule.EndingTime}";
            }
            else
            {
                scheduleLabel.Content = "Расписание: не задано";
            }
        }

        /// <summary>
        /// Обработчик добавления достижения участнику.
        /// </summary>
        private void AchivmentButton_Click(object sender, RoutedEventArgs e)
        {
            WIndowForMember wIndowForMember = new WIndowForMember(_context, _memberId);
            wIndowForMember.ShowDialog();

            DbLoad();
        }
    }
}
