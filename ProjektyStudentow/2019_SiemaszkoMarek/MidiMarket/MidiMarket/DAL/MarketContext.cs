using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MidiMarket.Models;
using System.Data.Entity;


namespace MidiMarket.DAL
{
    public class MarketContext : IdentityDbContext<ApplicationUser> //, ApplicationUserRoles
    {
        public MarketContext()
            : base("MyDbConnection", throwIfV1Schema: false)
        {
        }

        public static MarketContext Create()
        {
            return new MarketContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // wbudowane: DbSet<ApplicationUser> Users
    }
}