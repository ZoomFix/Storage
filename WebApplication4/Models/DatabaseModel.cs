using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace WebApplication.Models
{
    namespace DataAccessPostgreSqlProvider
    {
        // >dotnet ef migration add testMigration in AspNet5MultipleProject
        public class StorageDbContext : DbContext
        {
            public StorageDbContext()
            {

                Database.EnsureCreated();
            }

            public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
            {
            }

            public DbSet<DbStorage> StorageFacilities { get; set; }
            //public DbSet<DbFlight> Flights { get; set; }
            public static string ConnectionString { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql(StorageDbContext.ConnectionString);

                base.OnConfiguring(optionsBuilder);
            }
        }

        public class DbStorage
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            /// <summary>
            /// Название
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Вместимость
            /// </summary>
            public int Capacity { get; set; }
            /// <summary>
            /// Адрес
            /// </summary>
            public string Address { get; set; }
            /// <summary>
            /// Тип склада
            /// </summary>
            public Storage.StorageType StorageType { get; set; }
            /// <summary>
            /// Складской журнал
            /// </summary>
            public virtual Collection<DbItem> Items { get; set; }
            /// <summary>
            /// Работники
            /// </summary>
            public virtual Collection<DbWorkers> Workers { get; set; }
        }

        public class DbItem
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int StorageId { get; set; }
            [ForeignKey("StorageId")]

            public virtual DbStorage Storage { get; set; }
            /// <summary>
            /// Название
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Цена
            /// </summary>
            public int Price { get; set; }
            /// <summary>
            /// Кол-во
            /// </summary>
            public int Units { get; set; }
            /// <summary>
            /// Производитель
            /// </summary>
            public string Manufacturer { get; set; }

            public override string ToString()
            {
                return $"Название: {Name}, Цена: {Price}, Количество: {Units}, Производитель: {Manufacturer}";
            }
        }

        public class DbWorkers
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int StorageId { get; set; }
            [ForeignKey("StorageId")]
            public virtual DbStorage Storage { get; set; }
            /// <summary>
            /// Имя
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Должность
            /// </summary>
            public string Position { get; set; }
            /// <summary>
            /// Стаж
            /// </summary>
            public int Experience { get; set; }

            public override string ToString()
            {
                return $"Имя: {Name}, Должность: {Position}, Стаж: {Experience}";
            }
        }
    }
}
