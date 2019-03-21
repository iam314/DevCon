using DevConfSkopje.DataModels;
using System.Data.Entity;

namespace DevConfSkopje.Data
{
    public class DevConfDbContext: DbContext
    {
        public DevConfDbContext(): base("DevConfConnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ConferenceRegistration> ConferenceRegistrations { get; set; }

        public static DevConfDbContext Create()
        {
            return new DevConfDbContext();
        }

    }
}
