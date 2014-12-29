namespace FMIRatingsAPI.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using FMIRatingsAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FMIRatingsAPI.Models.FMIRatingsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FMIRatingsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

			var courses = new List<Course>() 
			{
				new Course() 
				{
					Name = "Data mining",
					Description = "Description Data Mining"
				},
				new Course() 
				{
					Name = "Intro to programming",
					Description = "Description Intro to programming"
				}
			};

			courses.ForEach(course => context.Courses.AddOrUpdate(course));
			context.SaveChanges();
        }
    }
}
