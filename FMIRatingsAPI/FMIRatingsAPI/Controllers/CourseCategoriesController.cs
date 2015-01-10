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
    public class CourseCategoriesController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

		// GET api/CourseCategories
		[ResponseType(typeof(List<CourseCategoryDTO>))]
		public List<CourseCategoryDTO> GetCourseCategories()
		{
			var courseCategories = db.CourseCategories.Select(category =>
				new CourseCategoryDTO()
				{
					Id = category.Id,
					Name = category.Name,
					Courses = category.Courses.Select(c => new CourseDTO()
					{
						Id = c.Id,
						Name = c.Name,
						Description = c.Description,
					}).ToList()
				}).ToList<CourseCategoryDTO>();

			return courseCategories;
		}

		//// GET api/CourseCategories/5
		//[ResponseType(typeof(CourseCategory))]
		//public async Task<IHttpActionResult> GetCourseCategory(int id)
		//{
		//	CourseCategory coursecategory = await db.CourseCategories.FindAsync(id);
		//	if (coursecategory == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(coursecategory);
		//}

		//// PUT api/CourseCategories/5
		//public async Task<IHttpActionResult> PutCourseCategory(int id, CourseCategory coursecategory)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != coursecategory.Id)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(coursecategory).State = EntityState.Modified;

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!CourseCategoryExists(id))
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

		//// POST api/CourseCategories
		//[ResponseType(typeof(CourseCategory))]
		//public async Task<IHttpActionResult> PostCourseCategory(CourseCategory coursecategory)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.CourseCategories.Add(coursecategory);

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateException)
		//	{
		//		if (CourseCategoryExists(coursecategory.Id))
		//		{
		//			return Conflict();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return CreatedAtRoute("DefaultApi", new { id = coursecategory.Id }, coursecategory);
		//}

		//// DELETE api/CourseCategories/5
		//[ResponseType(typeof(CourseCategory))]
		//public async Task<IHttpActionResult> DeleteCourseCategory(int id)
		//{
		//	CourseCategory coursecategory = await db.CourseCategories.FindAsync(id);
		//	if (coursecategory == null)
		//	{
		//		return NotFound();
		//	}

		//	db.CourseCategories.Remove(coursecategory);
		//	await db.SaveChangesAsync();

		//	return Ok(coursecategory);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		//private bool CourseCategoryExists(int id)
		//{
		//	return db.CourseCategories.Count(e => e.Id == id) > 0;
		//}
    }
}