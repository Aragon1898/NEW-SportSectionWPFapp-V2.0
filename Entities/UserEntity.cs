using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Entities
{
    /// <summary>
    /// Сущность пользователя в системе.
    /// Представляет базовую информацию о пользователе, независимо от его роли.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя в системе (Администратор/Тренер/Участник)
        /// </summary>
        public string Role { get; set; }
    }
}
