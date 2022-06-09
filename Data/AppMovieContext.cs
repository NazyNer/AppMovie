using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppMovie.Models;

    public class AppMovieContext : DbContext
    {
        public AppMovieContext (DbContextOptions<AppMovieContext> options)
            : base(options)
        {
        }

        public DbSet<AppMovie.Models.Section>? Section { get; set; }

        public DbSet<AppMovie.Models.Location>? Location { get; set; }

        public DbSet<AppMovie.Models.Producer>? Producer { get; set; }

        public DbSet<AppMovie.Models.Partner>? Partner { get; set; }

        public DbSet<AppMovie.Models.Country>? Country { get; set; }

        public DbSet<AppMovie.Models.Gender>? Gender { get; set; }

        public DbSet<AppMovie.Models.Movie>? Movie { get; set; }
    }
