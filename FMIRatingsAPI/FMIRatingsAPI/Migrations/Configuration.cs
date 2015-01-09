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
			        Id = 1,
			        Name = "Ядро на компютърни науки"
		        },
		        new CourseCategory()
		        {
			        Id = 2,
			        Name = "Основи на компютърни науки"
		        },
				new CourseCategory()
		        {
			        Id = 3,
			        Name = "Математика"
		        },
	        };

			courseCategories.ForEach(c => dbContext.CourseCategories.AddOrUpdate(category => category.Id, c));
			dbContext.SaveChanges();

			var courses = new List<Course>() 
			{
				new Course() 
				{
					Id = 1,
					Name = "Програмиране с .NET и C#",
					Description = "Курс за Програмиране с .NET и C#",
					Category = courseCategories[0]
				},
				new Course() 
				{
					Id = 2,
					Name = "Увод в програмирането",
					Description = "Курс за въведение в програмирането",
					Category = courseCategories[1]
				}
			};

			courses.ForEach(c => dbContext.Courses.AddOrUpdate(course => course.Id, c));
			dbContext.SaveChanges();

			var teachers = new List<Teacher>()
			{
				new Teacher()
				{
					Id = 1,
					Name = "Тодор Стоянов"
				},
				new Teacher()
				{
					Id = 2,
					Name = "Владимир Николов"
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
					Text = "Много добър преподавател.",
					TeacherId = teachers[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "Преподава доста добре.",
					TeacherId = teachers[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForTeacher()
				{
					Text = "Истински професионалист.",
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
					Text = "Курсът е много интересен.",
					CourseId = courses[0].Id,
                    UserId = users[0].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "Много добър курс.",
					CourseId = courses[1].Id,
                    UserId = users[1].Id,
					DateCreated = DateTime.Now,
				},
				new CommentForCourse()
				{
					Text = "Курсът е много полезен",
					CourseId = courses[1].Id,
                    UserId = users[1].Id,
					DateCreated = DateTime.Now,
				},
			};

            commentsForCourses.ForEach(c => dbContext.CommentsForCourses.AddOrUpdate(c));
            dbContext.SaveChanges();

            #region CriteriaForCourses
            var criteriaForCourses = new List<CriterionForCourse> {
                new CriterionForCourse()
                {
                    Id = 1,
                    Name = "Usefulness",
                    Description = "How useful is for practice"
                },
                new CriterionForCourse()
                {
                    Id = 2,
                    Name = "Simplicity",
                    Description = "How simple is for learning"
                },
                new CriterionForCourse()
                {
                    Id = 3,
                    Name = "Interest",
                    Description = "How interesting is for the students"
                },
                new CriterionForCourse()
                {
                    Id = 4,
                    Name = "Workload",
                    Description = "How many hours must separate for it"
                },
                new CriterionForCourse()
                {
                    Id = 5,
                    Name = "Clarity",
                    Description = "How clearness is for understanding"
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
                    Name = "Clarity",
                    Description = "How clearness the teacher is teaching"
                },
                new CriterionForTeacher()
                {
                    Id = 2,
                    Name = "Enthusiasm",
                    Description = "What level is the enthusiasm of the teacher for teaching"
                },
                new CriterionForTeacher()
                {
                    Id = 3,
                    Name = "Criteria of evaluation",
                    Description = "Is it bad or good teacher's evalution"
                },
                new CriterionForTeacher()
                {
                    Id = 4,
                    Name = "Speed of teaching",
                    Description = "How fast"
                },
                new CriterionForTeacher()
                {
                    Id = 5,
                    Name = "Scope of teaching material",
                    Description = "How much stuff the teacher is teaching"
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

            foreach (VoteForTeacher voteForTeacher in votesForTeachers)
            {
                var voteForTeacherInDb = dbContext.VotesForTeachers
                    .Where(t =>
                         t.UserId == voteForTeacher.UserId &&
                         t.Teacher.Id == voteForTeacher.TeacherId &&
                         t.Criterion.Id == voteForTeacher.CriterionId)
                    .SingleOrDefault();

                if (voteForTeacherInDb == null)
                {
                    dbContext.VotesForTeachers.Add(voteForTeacher);
                }
            }

            dbContext.SaveChanges();
            #endregion
        }
    }

}
