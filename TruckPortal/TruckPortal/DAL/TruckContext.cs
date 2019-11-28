using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TruckPortal.Entities;
using TruckPortal.Models;

namespace TruckPortal.DAL
{
    public class TruckContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=BRCTAW10351481\SQL2017;Database=TruckPortal;Integrated Security=True");
        }

        public DbSet<TruckPortal.Models.TruckViewModel> TruckViewModel { get; set; }
    }
}
