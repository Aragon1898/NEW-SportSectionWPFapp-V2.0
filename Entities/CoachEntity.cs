using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность тренера.
    /// Представляет пользователя с ролью тренера, который может вести спортивные секции.
    /// </summary>
    public class CoachEntity
    {
        /// <summary>
        /// Уникальный идентификатор тренера
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
        /// Отображаемое имя тренера (не сохраняется в базе данных)
        /// </summary>
        [NotMapped]
        public string DisplayName => User?.Name;
    }
}
