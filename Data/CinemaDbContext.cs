using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cinemaAPI.Models;

namespace cinemaAPI.Data
{

    public class CinemaDbContext: DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options): base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<User> Users { get; set; }
    }
}