using AtelierDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
namespace AtelierDatabaseImplement
{
    public class AtelierDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=Nelikey;Initial Catalog=AtelierDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Dress> Dresses { set; get; }
        public virtual DbSet<DressComponent> DressComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}