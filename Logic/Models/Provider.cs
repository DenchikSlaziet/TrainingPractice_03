using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Сущность поставщика
    /// </summary>
    public class Provider
    {
        public int ProviderId { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string Name { get; set; }
        public virtual ICollection<Fuel> Fuels { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
