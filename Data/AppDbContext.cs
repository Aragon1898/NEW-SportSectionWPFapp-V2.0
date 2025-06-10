using SportSectionWPF2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSectionWPF2.Data
{
    /// <summary>
    /// Контекст базы данных для спортивной секции.
    /// Обеспечивает доступ к таблицам и управление миграциями.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных.
        /// Настраивает автоматические миграции при запуске.
        /// </summary>
        public AppDbContext() : base("SportSectionDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Migrations.Configuration>());
        }

        /// <summary>
        /// Таблица пользователей системы
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Таблица достижений участников
        /// </summary>
        public DbSet<AchievementEntity> Achievements { get; set; }

        /// <summary>
        /// Таблица тренеров
        /// </summary>
        public DbSet<CoachEntity> Coachs { get; set; }

        /// <summary>
        /// Таблица посещаемости занятий
        /// </summary>
        public DbSet<AttendancesEntity> Attendances { get; set; }

        /// <summary>
        /// Таблица участников секции
        /// </summary>
        public DbSet<MemberEntity> Members { get; set; }

        /// <summary>
        /// Таблица расписания занятий
        /// </summary>
        public DbSet<ScheduleEntity> Schedules { get; set; }

        /// <summary>
        /// Таблица спортивных секций
        /// </summary>
        public DbSet<SectionEntity> Sections { get; set; }

        /// <summary>
        /// Таблица администраторов системы
        /// </summary>
        public DbSet<AdminEntity> Admins { get; set; }

        /// <summary>
        /// Настраивает связи между таблицами и дополнительные параметры модели.
        /// </summary>
        /// <param name="modelBuilder">Строитель модели Entity Framework</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SectionEntity>()
                .HasOptional(s => s.Schedule)
                .WithRequired(sch => sch.Section);

            base.OnModelCreating(modelBuilder);
        }
    }
}
