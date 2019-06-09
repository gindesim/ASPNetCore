using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoruCore22.Models
{
    public class DoruContext : DbContext
    {
        public DoruContext(DbContextOptions<DoruContext> options) : base(options)
        {
        }

        public DbSet<CastModel> CastSet { get; set; }

        public DbSet<CoverModel> CoverSet { get; set; }
    }
}
