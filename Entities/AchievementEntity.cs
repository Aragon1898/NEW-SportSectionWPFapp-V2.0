using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность достижения участника.
    /// Представляет достижения участника в соревнованиях, включая название соревнования, полученные баллы и награды.
    /// </summary>
    public class AchievementEntity
    {
        /// <summary>
        /// Уникальный идентификатор достижения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название соревнования
        /// </summary>
        public string CompetitionName { get; set; } = string.Empty;

        /// <summary>
        /// Количество набранных баллов
        /// </summary>
        public string Points { get; set; } = string.Empty;

        /// <summary>
        /// Полученные награды
        /// </summary>
        public string Awards { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор участника
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Связанный участник
        /// </summary>
        public MemberEntity Member { get; set; }
    }
}
