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
        /// Дата постройки
        /// </summary>
        public double Capacity { get; set; }
        /// <summary>
        /// Техническое обсуживание
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Тип корабля
        /// </summary>
        public StorageType StorageType { get; set; }
        /// <summary>
        /// Бортовой журнал
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

    /// <summary>
    /// Полёт
    /// </summary>
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

    /// <summary>
    /// Тип коробля
    /// </summary>
    public enum StorageType
    {
        Aplus,
        A,
        Bplus,
        B,
        C,
        D
    }

   
    
}
