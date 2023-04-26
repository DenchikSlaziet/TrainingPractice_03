using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    /// <summary>
    /// Сущность таблицы учета
    /// </summary>
    public class Account
    {
        public int AccountId { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Объем топлива на начало дня
        /// </summary>
        public decimal VolumeNew { get; set; }

        /// <summary>
        /// Объем остатка топлива
        /// </summary>
        public decimal VolumeSell { get; set; }

        /// <summary>
        /// Id топлива
        /// </summary>
        public int FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }

        public decimal FuelPrice => Fuel.Price;
        public string FuelProviderName => Fuel.Provider.Name;

    }
}
