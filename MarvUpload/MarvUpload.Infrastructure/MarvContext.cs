using MarvUpload.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvUpload.Infrastructure
{
    public class MarvContext : DbContext
    {
        public MarvContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

    }
}
