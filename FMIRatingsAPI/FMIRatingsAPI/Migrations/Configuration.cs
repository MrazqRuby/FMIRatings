namespace FMIRatingsAPI.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using FMIRatingsAPI.DAL;
	using FMIRatingsAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FMIRatingsContext>
    {
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(FMIRatingsContext dbContext)
		{
			var courses = new List<Course>() 
			{
				new Course() 
				{
					Id = 1,
					Name = "Data mining",
					Description = "Description Data Mining"
				},
				new Course() 
				{
					Id = 2,
					Name = "Intro to programming",
					Description = "Description Intro to programming"
				}
			};

			courses.ForEach(c => dbContext.Courses.AddOrUpdate(course => course.Name, c));
			dbContext.SaveChanges();

			var teachers = new List<Teacher>()
			{
				new Teacher()
				{
					Id = 1,
					Name = "John Williams",
					//Courses = courses
				},
				new Teacher()
				{
					Id = 2,
					Name = "Kent Beck",
					//Courses = courses
				}
			};
			teachers.ForEach(t => dbContext.Teachers.AddOrUpdate(teacher => teacher.Name, t));
			dbContext.SaveChanges();

			var teachersInCourses = new List<TeacherInCourse>()
			{
				new TeacherInCourse()
				{
					CourseId = courses[0].Id,
					TeacherId = teachers[0].Id,
				},
				new TeacherInCourse()
				{
					CourseId = courses[0].Id,
					TeacherId = teachers[1].Id,
				},
				new TeacherInCourse()
				{
					CourseId = courses[1].Id,
					TeacherId = teachers[0].Id,
				},
				new TeacherInCourse()
				{
					CourseId = courses[1].Id,
					TeacherId = teachers[1].Id,
				},
			};
			teachersInCourses.ForEach(t => dbContext.TeachersInCourses.AddOrUpdate(t));
			dbContext.SaveChanges();
			
        }
    }
}
