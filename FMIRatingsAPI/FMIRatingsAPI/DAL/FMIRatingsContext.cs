using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using FMIRatingsAPI.Models;

namespace FMIRatingsAPI.DAL
{
	public class FMIRatingsContext : DbContext
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, please use data migrations.
		// For more information refer to the documentation:
		// http://msdn.microsoft.com/en-us/data/jj591621.aspx

		public FMIRatingsContext()
			: base("name=FMIRatingsContext")
		{
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<TeacherInCourse> TeachersInCourses { get; set; }
		public DbSet<CommentForTeacher> CommentsForTeachers { get; set; }
		

	}
}
