using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    /// <summary>
    /// Сущность топлива 
    /// </summary>
    public class Fuel
    {
        public int FuelId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Id поставщика
        /// </summary>
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
