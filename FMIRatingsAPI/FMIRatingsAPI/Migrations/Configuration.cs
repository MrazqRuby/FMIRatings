namespace FMIRatingsAPI.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using FMIRatingsAPI.DAL;
	using FMIRatingsAPI.Models;
	using FMIRatingsAPI.Models.DTO;

	internal sealed class Configuration : DbMigrationsConfiguration<FMIRatingsAPI.DAL.FMIRatingsContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(FMIRatingsContext dbContext)
		{
			var courseCategories = new List<CourseCategory>()
	        {
		        new CourseCategory()
		        {
			        CategoryId = 1,
			        Name = "Ядро на компютърни науки"
		        },
		        new CourseCategory()
		        {
			        CategoryId = 2,
			        Name = "Основи на компютърни науки"
		        },
				new CourseCategory()
		        {
			        CategoryId = 3,
			        Name = "Математика"
		        },
	        };

			courseCategories.ForEach(c => dbContext.CourseCategories.AddOrUpdate(category => category.CategoryId, c));
			dbContext.SaveChanges();

			var courses = new List<Course>() 
			{
				new Course() 
				{
					Id = 1,
					Name = "Увод в програмирането",
					Description = "Курс по Увод в програмирането",
					CategoryId = courseCategories[0].CategoryId
				},
				new Course() 
				{
					Id = 2,
					Name = "Обектно-ориентирано програмиране",
					Description = "Курс по Обектно-ориентирано програмиране",
					CategoryId = courseCategories[1].CategoryId
				}
			};

			courses.ForEach(c => dbContext.Courses.AddOrUpdate(course => course.Id, c));
			dbContext.SaveChanges();

			var teacherDepartments = new List<TeacherDepartment>()
	        {
		        new TeacherDepartment()
		        {
			        DepartmentId = 1,
			        Name = "Вероятности и статистика"
		        },
		        new TeacherDepartment()
		        {
			        DepartmentId = 2,
			        Name = "Анализ"
		        },
				new TeacherDepartment()
		        {
			        DepartmentId = 3,
			        Name = "Софтуерни технологии"
		        },
	        };

			teacherDepartments.ForEach(d => dbContext.TeacherDepartments.AddOrUpdate(department => department.DepartmentId, d));
			dbContext.SaveChanges();

			var teachers = new List<Teacher>()
			{
				new Teacher()
				{
					Id = 1,
					Name = "Тодор Стоянов",
					DepartmentId = teacherDepartments[0].DepartmentId
				},
				new Teacher()
				{
					Id = 2,
					Name = "Владимир Николов",
					DepartmentId = teacherDepartments[2].DepartmentId
				}
			};
			teachers.ForEach(t => dbContext.Teachers.AddOrUpdate(teacher => teacher.Id, t));
			dbContext.SaveChanges();

			var users = new List<User>()
			{
				new User()
				{
					Id = 1,
					Name = "admin",
                    Password = "admin",
                    Admin = true
				},
				new User()
				{
					Id = 2,
					Name = "user",
                    Password = "user",
                    Admin = false
				}
			};
			users.ForEach(u => dbContext.Users.AddOrUpdate(user => user.Name, u));
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
					Text = "Много добър преподавател",
					TeacherId = teachers[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "Истински професионалист",
					TeacherId = teachers[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "Специалист в областта",
					TeacherId = teachers[1].Id,
                    UserId = users[1].Id,
					DateCreated = DateTime.Now,
				},
			};

			commentsForTeachers.ForEach(c => dbContext.CommentsForTeachers.AddOrUpdate(c));
			dbContext.SaveChanges();

			var commentsForCourses = new List<CommentForCourse>()
			{
				new CommentForCourse()
				{
					Text = "Много добър курс",
					CourseId = courses[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "Курсът е много полезен",
					CourseId = courses[1].Id,
                    UserId = users[1].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "",
					CourseId = courses[1].Id,
                    UserId = users[1].Id,
					DateCreated = DateTime.Now,
				},
			};

			commentsForCourses.ForEach(c => dbContext.CommentsForCourses.AddOrUpdate(c));

            var statistics = new List<Statistic>()
            {
                new Statistic()
                {
                    Id = 1,
                    Name = "Най-голем пич",
                    FileName = "dude",
                    InputTransform = "teacherNames: String = Teachers.Select(t => t.Name)",
                    InputType = "Teacher",
                    ReturnType = Statistic.ResultType.IMAGE
                },
                new Statistic()
                {
                    Id = 2,
                    Name = "Най-голем бастон",
                    FileName = "staff",
                    InputTransform = @"teacherNames: System.String = Teachers.Select(Name)
teacherIds: System.Int32 = Teachers.Select(t => t.Id)
teacherVote: System.Collections.Generic.List`1[System.Int32] = VotesForTeachers.Where(v => v.Criterion.Name == ""Яснота"").GroupBy(v => v.TeacherId, v => v.Assessment, (key, val) => val.ToList()).ToList();",
                    InputType = "Teacher",
                    ReturnType = Statistic.ResultType.IMAGE
                }
            };

            statistics.ForEach(s => dbContext.Statistics.AddOrUpdate(s));

			dbContext.SaveChanges();

			#region CriteriaForCourses
			var criteriaForCourses = new List<CriterionForCourse> {
                new CriterionForCourse()
                {
                    Id = 1,
                    Name = "Полезност",
                    Description = "Колко е полезен практически"
                },
                new CriterionForCourse()
                {
                    Id = 2,
                    Name = "Леснота",
                    Description = "Колко лесен е курсът"
                },
                new CriterionForCourse()
                {
                    Id = 3,
                    Name = "Интерес",
                    Description = "Колко интересен е курсът"
                },
                new CriterionForCourse()
                {
                    Id = 4,
                    Name = "Натовареност",
                    Description = "Колко натоварващ е курсът"
                },
                new CriterionForCourse()
                {
                    Id = 5,
                    Name = "Яснота",
                    Description = "Колко е ясен курсът за доброто разбиране"
                }
            };

			criteriaForCourses.ForEach(c =>
				dbContext.CriteriaForCourses.AddOrUpdate(c));
			dbContext.SaveChanges();
			#endregion

			#region CriteriaForTeachers
			var criteriaForTeachers = new List<CriterionForTeacher> {
                new CriterionForTeacher()
                {
                    Id = 1,
                    Name = "Яснота",
                    Description = "Колко ясно се преподава"
                },
                new CriterionForTeacher()
                {
                    Id = 2,
                    Name = "Ентусиазъм",
                    Description = "Колко ентусиезиран е преподавателят в работата си"
                },
                new CriterionForTeacher()
                {
                    Id = 3,
                    Name = "Критерии на оценяване",
                    Description = "Колко добре оценява преподавателят"
                },
                new CriterionForTeacher()
                {
                    Id = 4,
                    Name = "Скорост на преподаване",
                    Description = "Колко бързо преподава"
                },
                new CriterionForTeacher()
                {
                    Id = 5,
                    Name = "Обхват на преподавания материал",
                    Description = "Колко количество материал се преподава"
                }
            };

			criteriaForTeachers.ForEach(c =>
				dbContext.CriteriaForTeachers.AddOrUpdate(c));
			dbContext.SaveChanges();
			#endregion

			#region VotesForCourses
			var votesForCourses = new List<VoteForCourse>()
            {
                new VoteForCourse()
                {
                    UserId = 0,
                    CourseId = courses[0].Id,
                    CriterionId = criteriaForCourses[0].Id,
                    Assessment =  (int)Assessment.Three  
                },
                new VoteForCourse()
                {
                    UserId = 0,
                    CourseId = courses[0].Id,
                    CriterionId = criteriaForCourses[1].Id,
                    Assessment =  (int)Assessment.Four  
                },
                new VoteForCourse()
                {
                    UserId = 0,
                    CourseId = courses[0].Id,
                    CriterionId = criteriaForCourses[4].Id,
                    Assessment = (int)Assessment.Five  
                },
                new VoteForCourse()
                {
                    UserId = -1,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[1].Id,
                    Assessment =  (int)Assessment.One  
                },
                 new VoteForCourse()
                {
                    UserId = -2,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[1].Id,
                    Assessment =  (int)Assessment.Two  
                },
                 new VoteForCourse()
                {
                    UserId = -3,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[1].Id,
                    Assessment =  (int)Assessment.Five  
                },
                 new VoteForCourse()
                {

                    UserId = 0,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[2].Id,
                    Assessment =  (int)Assessment.Two  
                },
                 new VoteForCourse()
                {
                    UserId = 0,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[3].Id,
                    Assessment =  (int)Assessment.Four  
                },
                 new VoteForCourse()
                {
                    UserId = 0,
                    CourseId = courses[1].Id,
                    CriterionId = criteriaForCourses[4].Id,
                    Assessment =  (int)Assessment.One  
                },
            };

			foreach (VoteForCourse voteForCourse in votesForCourses)
			{
				var voteForCourseInDb = dbContext.VotesForCourses
					.Where(t =>
						 t.UserId == voteForCourse.UserId &&
						 t.Course.Id == voteForCourse.CourseId &&
						 t.Criterion.Id == voteForCourse.CriterionId)
					.SingleOrDefault();

				if (voteForCourseInDb == null)
				{
					dbContext.VotesForCourses.Add(voteForCourse);
				}
			}

			dbContext.SaveChanges();
			#endregion

			#region VotesForTeachers
			var votesForTeachers = new List<VoteForTeacher>()
            {
                new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[0].Id,
                    CriterionId = criteriaForTeachers[0].Id,
                    Assessment = (int)Assessment.Three  
                },
                new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[0].Id,
                    CriterionId = criteriaForTeachers[1].Id,
                    Assessment = (int)Assessment.Four  
                },
                new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[0].Id,
                    CriterionId = criteriaForTeachers[4].Id,
                    Assessment = (int)Assessment.Five  
                },
                new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[1].Id,
                    Assessment = (int)Assessment.One  
                },
                 new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[1].Id,
                    Assessment = (int)Assessment.Two  
                },
                 new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[1].Id,
                    Assessment = (int)Assessment.Five  
                },
                 new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[2].Id,
                    Assessment = (int)Assessment.Two  
                },
                 new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[3].Id,
                    Assessment = (int)Assessment.Four  
                },
                 new VoteForTeacher()
                {
                    UserId = 1,
                    TeacherId = teachers[1].Id,
                    CriterionId = criteriaForTeachers[4].Id,
                    Assessment = (int)Assessment.One  
                },
            };

            votesForTeachers.ForEach(v => dbContext.VotesForTeachers.AddOrUpdate(v));

			dbContext.SaveChanges();
			#endregion
		}
	}

}
