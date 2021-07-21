using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevelopmentProject.Models;

namespace DevelopmentProject.Data
{
    public class DevProjMainContext : DbContext
    {
        public DevProjMainContext (DbContextOptions<DevProjMainContext> options)
            : base(options)
        {
        }

        public DbSet<DevelopmentProject.Models.Customer> Customer { get; set; }

        public DbSet<DevelopmentProject.Models.OccupationRating> OccupationRating { get; set; }

        public DbSet<DevelopmentProject.Models.RatingFactor> RatingFactor { get; set; }        
    }
}
