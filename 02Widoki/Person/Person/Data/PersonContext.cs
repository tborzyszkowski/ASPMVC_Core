using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext (DbContextOptions<PersonContext> options)
            : base(options)
        {
        }

        public DbSet<Person.Models.Human> Human { get; set; }

        public DbSet<Person.Models.Address> Address { get; set; }
    }
}
