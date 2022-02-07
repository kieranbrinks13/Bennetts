using BennettsDataModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BennettsData
{
    public class BennettsDbContext : DbContext
    {
        public BennettsDbContext() : base("DbConnection")
        {

        }

        public DbSet<UserDM> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}