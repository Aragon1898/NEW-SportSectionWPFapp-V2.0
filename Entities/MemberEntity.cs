using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность участника спортивной секции.
    /// Представляет пользователя, который может посещать секции и иметь достижения.
    /// </summary>
    public class MemberEntity
    {
        /// <summary>
        /// Уникальный идентификатор участника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор связанного пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Связанный пользователь системы
        /// </summary>
        public UserEntity User { get; set; }

        /// <summary>
        /// Отображаемое имя участника (не сохраняется в базе данных)
        /// </summary>
        [NotMapped]
        public string DisplayName => User?.Name;

        /// <summary>
        /// Список секций, в которых участвует пользователь
        /// </summary>
        public List<SectionEntity> Sections { get; set; }

        /// <summary>
        /// Список достижений участника
        /// </summary>
        public List<AchievementEntity> Achievements { get; set; }

        /// <summary>
        /// История посещений занятий
        /// </summary>
        public List<AttendancesEntity> Attendances { get; set; }
    }
}
