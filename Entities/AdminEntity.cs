using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность администратора системы.
    /// Представляет пользователя с правами администратора, который может управлять секциями, тренерами и участниками.
    /// </summary>
    public class AdminEntity
    {
        /// <summary>
        /// Уникальный идентификатор администратора
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
    }
}
