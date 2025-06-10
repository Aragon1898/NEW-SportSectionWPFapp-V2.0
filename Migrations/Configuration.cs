namespace SportSectionWPF2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Конфигурация миграций базы данных.
    /// Определяет настройки для автоматических миграций Entity Framework.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<SportSectionWPF2.Data.AppDbContext>
    {
        /// <summary>
        /// Инициализирует новый экземпляр конфигурации миграций.
        /// Включает автоматические миграции и разрешает потенциальную потерю данных.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Метод для заполнения базы данных начальными данными.
        /// Вызывается после применения всех миграций.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        protected override void Seed(SportSectionWPF2.Data.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
