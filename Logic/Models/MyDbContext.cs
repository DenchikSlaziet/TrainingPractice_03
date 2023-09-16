using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext():base("DbConectionStringHome")
        { 
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
