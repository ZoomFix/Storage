using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public class StorageModel
    {
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
        public StorageType StorageType { get; set; }
        /// <summary>
        /// Складской журнал
        /// </summary>
        public List<Items> Items { get; set; }
        /// <summary>
        /// Работники
        /// </summary>
        public List<Workers> Workers { get; set; }
    }

    public class Workers
    {
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

    public class Items
    {
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

    public enum StorageType
    {
        /// <summary>
        /// Классификация складов
        /// </summary>
        Aplus,
        A,
        Bplus,
        B,
        C,
        D
    }
}
