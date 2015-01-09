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
using FMIRatingsAPI.Models;
using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models.DTO;

namespace FMIRatingsAPI.Controllers
{
    public class CourseCommentsController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/CourseComments
        public IQueryable<CommentForCourseDTO> GetCommentsForCourses()
        {
			return db.CommentsForCourses.Select(comment => new CommentForCourseDTO()
			{
				Id = comment.Id,
				Author = "Stamo",
				CourseId = comment.Id,
				DateCreated = comment.DateCreated,
				Text = comment.Text
			});
        }

        // GET api/CourseComments/5
		[ResponseType(typeof(List<CommentForCourseDTO>))]
		public List<CommentForCourseDTO> GetCommentsForCourse(int id)
        {
			List<CommentForCourseDTO> commentsForCourse = db.CommentsForCourses
				.Where(comment => comment.CourseId == id)
				.Select(comment => new CommentForCourseDTO()
				{
					Id = comment.Id,
					Author = "Stamo",
					CourseId = comment.Id,
					DateCreated = comment.DateCreated,
					Text = comment.Text
				}).ToList();

			return commentsForCourse;
        }


		// POST api/CourseComments
		[ResponseType(typeof(CommentForCourseDTO))]
		public async Task<IHttpActionResult> PostCommentForTeacher([FromBody]CommentForCourseDTO commentForCourse)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var course = db.Courses
				.Where(c => c.Id == commentForCourse.CourseId)
				.SingleOrDefaultAsync();

			if (course != null)
			{
				db.CommentsForCourses.Add(new CommentForCourse()
				{
					CourseId = commentForCourse.CourseId,
					Text = commentForCourse.Text,
					DateCreated = DateTime.Now,
				});

				try
				{
					await db.SaveChangesAsync();
				}
				catch (DbUpdateException)
				{
					if (CommentForCourseExists(commentForCourse.Id))
					{
						return Conflict();
					}
					else
					{
						throw;
					}
				}

				return CreatedAtRoute("DefaultApi", new { id = commentForCourse.Id }, commentForCourse);
			}
			else
			{
				return NotFound();
			}

			
		}

		//// POST api/CourseComments
		//[ResponseType(typeof(CommentForCourse))]
		//public async Task<IHttpActionResult> PostCommentForCourse(CommentForCourse commentforcourse)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.CommentsForCourses.Add(commentforcourse);
		//	await db.SaveChangesAsync();

		//	return CreatedAtRoute("DefaultApi", new { id = commentforcourse.Id }, commentforcourse);
		//}

		//// DELETE api/CourseComments/5
		//[ResponseType(typeof(CommentForCourse))]
		//public async Task<IHttpActionResult> DeleteCommentForCourse(int id)
		//{
		//	CommentForCourse commentforcourse = await db.CommentsForCourses.FindAsync(id);
		//	if (commentforcourse == null)
		//	{
		//		return NotFound();
		//	}

		//	db.CommentsForCourses.Remove(commentforcourse);
		//	await db.SaveChangesAsync();

		//	return Ok(commentforcourse);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		private bool CommentForCourseExists(int id)
		{
			return db.CommentsForCourses.Count(e => e.Id == id) > 0;
		}
    }
}