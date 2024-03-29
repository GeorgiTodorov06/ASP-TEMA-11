using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class WebApplication6Context : DbContext
    {
        public WebApplication6Context (DbContextOptions<WebApplication6Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication6.Models.Doctor> Doctor { get; set; } = default!;

        public DbSet<WebApplication6.Models.Pacient>? Pacient { get; set; }

        public DbSet<WebApplication6.Models.Visit> Visit { get; set; }
    }
}
