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
    public class TeacherCommentsController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

		// GET api/TeacherComments
		public IQueryable<CommentForTeacherDTO> GetCommentsForTeachers()
		{
			return db.CommentsForTeachers.Select(comment => new CommentForTeacherDTO()
			{
				Id = comment.Id,
				Author = "Stamo",
				TeacherId = comment.TeacherId,
				DateCreated = comment.DateCreated,
				Text = comment.Text
			});
		}

		// GET api/TeacherComments/5
		[ResponseType(typeof(List<CommentForTeacherDTO>))]
		public List<CommentForTeacherDTO> GetCommentsForTeacher(int id)
		{
			List<CommentForTeacherDTO> commentsForTeacher = db.CommentsForTeachers
				.Where(comment => comment.TeacherId == id)
				.Select(comment => new CommentForTeacherDTO()
				{
					Id = comment.Id,
					Author = "Stamo",
					TeacherId = comment.Id,
					DateCreated = comment.DateCreated,
					Text = comment.Text
				}).ToList();

			return commentsForTeacher;
		}

		//// PUT api/TeacherComments/5
		//public async Task<IHttpActionResult> PutCommentForTeacher(int id, CommentForTeacher commentforteacher)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != commentforteacher.Id)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(commentforteacher).State = EntityState.Modified;

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!CommentForTeacherExists(id))
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

        // POST api/TeacherComments
        [ResponseType(typeof(CommentForTeacherDTO))]
		public async Task<IHttpActionResult> PostCommentForTeacher([FromBody]CommentForTeacherDTO commentForTeacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var teacher = db.Teachers
				.Where(t => t.Id == commentForTeacher.TeacherId)
				.SingleOrDefaultAsync();

			if (teacher != null)
			{
				db.CommentsForTeachers.Add(new CommentForTeacher()
					{
						TeacherId = commentForTeacher.TeacherId,
						Text =  commentForTeacher.Text,
						DateCreated = DateTime.Now,
					});
			}

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentForTeacherExists(commentForTeacher.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = commentForTeacher.Id }, commentForTeacher);
        }

		//// DELETE api/TeacherComments/5
		//[ResponseType(typeof(CommentForTeacher))]
		//public async Task<IHttpActionResult> DeleteCommentForTeacher(int id)
		//{
		//	CommentForTeacher commentforteacher = await db.CommentsForTeachers.FindAsync(id);
		//	if (commentforteacher == null)
		//	{
		//		return NotFound();
		//	}

		//	db.CommentsForTeachers.Remove(commentforteacher);
		//	await db.SaveChangesAsync();

		//	return Ok(commentforteacher);
		//}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentForTeacherExists(int id)
        {
            return db.CommentsForTeachers.Count(e => e.Id == id) > 0;
        }
    }
}