using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность посещаемости.
    /// Представляет запись о посещении участником занятия в определенную дату.
    /// </summary>
    public class AttendancesEntity
    {
        /// <summary>
        /// Уникальный идентификатор записи посещаемости
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата занятия
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Флаг присутствия на занятии
        /// </summary>
        public bool IsPresent { get; set; }

        /// <summary>
        /// Связанное расписание занятия
        /// </summary>
        public ScheduleEntity Schedule { get; set; }
    }
}
