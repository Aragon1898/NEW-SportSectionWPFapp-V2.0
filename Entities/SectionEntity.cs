using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность спортивной секции.
    /// Представляет спортивную секцию с тренером, участниками и расписанием занятий.
    /// </summary>
    public class SectionEntity
    {
        /// <summary>
        /// Уникальный идентификатор секции
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название спортивной секции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор тренера, ведущего секцию
        /// </summary>
        public int? CoachId { get; set; }

        /// <summary>
        /// Тренер, ведущий секцию
        /// </summary>
        public CoachEntity Coach { get; set; }

        /// <summary>
        /// Список участников секции
        /// </summary>
        public List<MemberEntity> Members { get; set; }

        /// <summary>
        /// Расписание занятий секции
        /// </summary>
        public ScheduleEntity Schedule { get; set; }
    }
}
