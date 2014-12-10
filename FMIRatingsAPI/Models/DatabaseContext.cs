using System.Data.Entity;

namespace FMIRatingsAPI.Models
{
    public class DatabaseContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FMIRatingsAPI.Models.DatabaseContext>());

        public DatabaseContext() : base("name=DatabaseConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Discipline>().HasMany(d => d.Tutors).WithMany(t => t.CoursesTaught);
        }

    }
}
