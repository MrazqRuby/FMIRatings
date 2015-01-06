namespace FMIRatingsAPI.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using FMIRatingsAPI.DAL;
	using FMIRatingsAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FMIRatingsAPI.DAL.FMIRatingsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
					Name = "John Williams"
				},
				new Teacher()
				{
					Id = 2,
					Name = "Kent Beck"
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

			foreach (TeacherInCourse teacherInCourse in teachersInCourses)
			{
				var teacherInCourseInDb = dbContext.TeachersInCourses
					.Where(t =>
						 t.Teacher.Id == teacherInCourse.TeacherId &&
						 t.Course.Id == teacherInCourse.CourseId)
					.SingleOrDefault();

				if (teacherInCourseInDb == null)
				{
					dbContext.TeachersInCourses.Add(teacherInCourse);
				}
			}

			dbContext.SaveChanges();

			var commentsForTeachers = new List<CommentForTeacher>()
			{
				new CommentForTeacher()
				{
					Text = "comment for a teacher",
					TeacherId = teachers[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "another comment for a teacher",
					TeacherId = teachers[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "a very good teacher",
					TeacherId = teachers[1].Id,
					DateCreated = DateTime.Now,
				},
			};

			commentsForTeachers.ForEach(c => dbContext.CommentsForTeachers.AddOrUpdate(c));
			dbContext.SaveChanges();

			var commentsForCourses = new List<CommentForCourse>()
			{
				new CommentForCourse()
				{
					Text = "comment for a course",
					CourseId = courses[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "another comment for a course",
					CourseId = courses[1].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "a very good course",
					CourseId = courses[1].Id,
					DateCreated = DateTime.Now,
				},
			};

			commentsForCourses.ForEach(c => dbContext.CommentsForCourses.AddOrUpdate(c));
			dbContext.SaveChanges();
		}
	}
    
}
