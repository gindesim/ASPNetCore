using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoruReactAPICore22.Models
{
    public partial class DoruContext : DbContext
    {
        public DoruContext()
        {
        }

        public DoruContext(DbContextOptions<DoruContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CastModel> Cast { get; set; }
        public virtual DbSet<CoverModel> Cover { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DIAMOND;Database=Doru;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");
        }
    }
}
