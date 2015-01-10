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
	public class CoursesController : ApiController
	{
		private FMIRatingsContext db = new FMIRatingsContext();

		// GET api/Courses
		[ResponseType(typeof(List<CourseDTO>))]
		public List<CourseDTO> GetCourses()
		{
			var courses = db.Courses.Select(course =>
				new CourseDTO()
				{
					Id = course.Id,
					Name = course.Name,
					Description = course.Description,
					Teachers = course.Teachers.Select(teacher =>
						teacher.Teacher.Name).ToList<string>(),
					Comments = course.Comments.Select(comment =>
						new CommentForCourseDTO()
						{
							Id = comment.Id,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = comment.User.Name
						}).ToList<CommentForCourseDTO>(),
					Category = course.Category.Name
				}).ToList();

			return courses;
		}

		// GET api/Courses/5
		[ResponseType(typeof(CourseDTO))]
		public IHttpActionResult GetCourse(int id)
		{
			var courseInDb = db.Courses.SingleOrDefault(c => c.Id == id);
			if (courseInDb != null)
			{
				CourseDTO course = db.Courses
				.Where(c => c.Id == id)
				.Select(c => new CourseDTO()
				{
					Id = c.Id,
					Name = c.Name,
					Description = c.Description,
					Teachers = c.Teachers.Select(t => t.Teacher.Name).ToList<string>(),
					Comments = c.Comments.Select(comment =>
						new CommentForCourseDTO()
						{
							Id = comment.Id,
							CourseId = comment.CourseId,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = comment.User.Name
						}).ToList<CommentForCourseDTO>(),
					Category = c.Category.Name
				}).SingleOrDefault();

				return Ok(course);
			}
			
			return NotFound();
		}

		// GET api/Courses
		[HttpGet]
		[ResponseType(typeof(List<CourseDTO>))]
		public List<CourseDTO> Search(string name)
		{
			var courses = db.Courses
				.Where(course => course.Name.Contains(name))
				.Select(course =>
				new CourseDTO()
				{
					Id = course.Id,
					Name = course.Name,
					Description = course.Description,
					Teachers = course.Teachers.Select(teacher =>
						teacher.Teacher.Name).ToList<string>(),
					Comments = course.Comments.Select(comment =>
						new CommentForCourseDTO()
						{
							Id = comment.Id,
							Text = comment.Text,
							DateCreated = comment.DateCreated,
							Author = comment.User.Name
						}).ToList<CommentForCourseDTO>(),
					Category = course.Category.Name
				}).ToList();

			return courses;
		}

		//// PUT api/Courses/5
		//public async Task<IHttpActionResult> PutCourse(int id, Course course)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != course.Id)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(course).State = EntityState.Modified;

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!CourseExists(id))
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

		//// POST api/Courses
		//[ResponseType(typeof(Course))]
		//public async Task<IHttpActionResult> PostCourse(Course course)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.Courses.Add(course);
		//	await db.SaveChangesAsync();

		//	return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
		//}

		//// DELETE api/Courses/5
		//[ResponseType(typeof(Course))]
		//public async Task<IHttpActionResult> DeleteCourse(int id)
		//{
		//	Course course = await db.Courses.FindAsync(id);
		//	if (course == null)
		//	{
		//		return NotFound();
		//	}

		//	db.Courses.Remove(course);
		//	await db.SaveChangesAsync();

		//	return Ok(course);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		//private bool CourseExists(int id)
		//{
		//	return db.Courses.Count(e => e.Id == id) > 0;
		//}
	}
}