using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность расписания занятий.
    /// Представляет расписание занятий для спортивной секции с учетом времени начала и окончания.
    /// </summary>
    public class ScheduleEntity
    {
        /// <summary>
        /// Уникальный идентификатор расписания
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время начала занятия (DateTime)
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания занятия (DateTime)
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Строковое представление времени начала занятия
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// Строковое представление времени окончания занятия
        /// </summary>
        public string EndingTime { get; set; }

        /// <summary>
        /// Идентификатор связанной секции
        /// </summary>
        public int SectionEntityId { get; set; }

        /// <summary>
        /// Связанная спортивная секция
        /// </summary>
        public SectionEntity Section { get; set; }

        /// <summary>
        /// Список посещений по данному расписанию
        /// </summary>
        public List<AttendancesEntity> Attendances { get; set; }
    }
}
