using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;

namespace FMIRatingsAPI.Controllers
{
	//[FMIRatingsAPI.Authentication.AuthenticationFilter]
    public class TeachersController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

		[HttpGet]
        // GET api/Teachers
		[ResponseType(typeof(List<TeacherDTO>))]
        public List<TeacherDTO> GetTeachers()
        {
			var teachers = db.Teachers.Select(teacher =>
				new TeacherDTO()
				{
					Id = teacher.Id,
					Name = teacher.Name,
					Courses = teacher.Courses.Select(course => new CourseDTO()
					{
						Id = course.Course.Id,
						Name = course.Course.Name,
						
					}).ToList<CourseDTO>(),
					Comments = teacher.Comments.Select(comment =>
						new CommentForTeacherDTO()
						{
							Id = comment.Id,
							TeacherId = comment.TeacherId,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = comment.User.Name
                        }).ToList<CommentForTeacherDTO>(),
					Department = teacher.Department.Name
				}).ToList();

			return teachers;
        }

		// GET api/Teachers/5
		[ResponseType(typeof(TeacherDTO))]
		public IHttpActionResult GetTeacher(int id)
		{
			var teacherInDb = db.Teachers.SingleOrDefault(t => t.Id == id);
			if (teacherInDb != null)
			{
				TeacherDTO teacher = db.Teachers
				.Where(t => t.Id == id)
				.Select(t => new TeacherDTO()
				{
					Id = t.Id,
					Name = t.Name,
					Courses = t.Courses.Select(course => new CourseDTO()
					{
						Id = course.Course.Id,
						Name = course.Course.Name,
					}).ToList<CourseDTO>(),
					Comments = t.Comments.Select(comment =>
						new CommentForTeacherDTO()
						{
							Id = comment.Id,
							TeacherId = comment.TeacherId,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = "Stamo"
						}).ToList<CommentForTeacherDTO>(),
					Department = t.Department.Name
				}).SingleOrDefault();

				return Ok(teacher);
			}
			else
			{
				return NotFound();
			}
		}

		// GET api/Teachers
		[HttpGet]
		[ResponseType(typeof(List<TeacherDTO>))]
		public List<TeacherDTO> Search(string name)
		{
			var teachers = db.Teachers
				.Where(t => t.Name.Contains(name))
				.Select(teacher =>
				new TeacherDTO()
				{
					Id = teacher.Id,
					Name = teacher.Name,
					Courses = teacher.Courses.Select(course => new CourseDTO()
					{
						Id = course.Course.Id,
						Name = course.Course.Name,

					}).ToList<CourseDTO>(),
					Comments = teacher.Comments.Select(comment =>
						new CommentForTeacherDTO()
						{
							Id = comment.Id,
							TeacherId = comment.TeacherId,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = comment.User.Name
						}).ToList<CommentForTeacherDTO>(),
					Department = teacher.Department.Name
				}).ToList();

			return teachers;
		}

		//// PUT api/Teachers/5
		//public IHttpActionResult PutTeacher(int id, Teacher teacher)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != teacher.Id)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(teacher).State = EntityState.Modified;

		//	try
		//	{
		//		db.SaveChanges();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!TeacherExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return StatusCode(HttpStatusCode.NoContent);
		//}

		//// POST api/Teachers
		//[ResponseType(typeof(Teacher))]
		//public IHttpActionResult PostTeacher(Teacher teacher)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.Teachers.Add(teacher);
		//	db.SaveChanges();

		//	return CreatedAtRoute("DefaultApi", new { id = teacher.Id }, teacher);
		//}

		//// DELETE api/Teachers/5
		//[ResponseType(typeof(Teacher))]
		//public IHttpActionResult DeleteTeacher(int id)
		//{
		//	Teacher teacher = db.Teachers.Find(id);
		//	if (teacher == null)
		//	{
		//		return NotFound();
		//	}

		//	db.Teachers.Remove(teacher);
		//	db.SaveChanges();

		//	return Ok(teacher);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		//private bool TeacherExists(int id)
		//{
		//	return db.Teachers.Count(e => e.Id == id) > 0;
		//}
    }
}