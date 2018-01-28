using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proj4.Models;
using Microsoft.EntityFrameworkCore;

namespace proj4.Data {
    public class MusicContext : DbContext {
        public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }

        public DbSet<Listener> Listeners { get; set; }
        public DbSet<BandListener> BandsListeners { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Listener>().ToTable("listener");
            modelBuilder.Entity<Band>().ToTable("band");
            //modelBuilder.Entity<Band>().Haso
            modelBuilder.Entity<BandListener>().ToTable("band_listener");
            modelBuilder.Entity<Tour>().ToTable("tour");
            //.HasKey(t => new { t.TourID });
            modelBuilder.Entity<BandListener>()
                .HasKey(bl => new { bl.BandID, bl.ListenerID });
        }
    }
}
