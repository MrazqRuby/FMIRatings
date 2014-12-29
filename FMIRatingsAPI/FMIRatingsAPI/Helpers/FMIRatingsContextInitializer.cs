using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FMIRatingsAPI.Models;
using System.Data.Entity.Migrations;

namespace FMIRatingsAPI.Helpers
{
	public class FMIRatingsContextInitializer
				: DropCreateDatabaseAlways<FMIRatingsContext>
	{
		protected override void Seed(FMIRatingsContext context)
		{
			var courses = new List<Course>() 
			{
				new Course() 
				{
					Name = "Data mining",
					Description = "Description Data Mining"
				}
			};

			courses.ForEach(course => context.Courses.AddOrUpdate(course));
			context.SaveChanges();

			base.Seed(context);
		}
	}
}