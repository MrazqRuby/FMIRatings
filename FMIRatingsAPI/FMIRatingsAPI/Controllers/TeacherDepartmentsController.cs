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
    public class TeacherDepartmentsController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/TeacherDepartments
		[ResponseType(typeof(List<TeacherDepartmentDTO>))]
		public List<TeacherDepartmentDTO> GetCourseCategories()
		{
			var teacherDepartments = db.TeacherDepartments.Select(department =>
				new TeacherDepartmentDTO()
				{
					Id = department.DepartmentId,
					Name = department.Name,
					Teachers = department.Teachers.Select(c => new TeacherDTO()
					{
						Id = c.Id,
						Name = c.Name
					}).ToList()
				}).ToList<TeacherDepartmentDTO>();

			return teacherDepartments;
		}

		//// GET api/TeacherDepartments/5
		//[ResponseType(typeof(TeacherDepartment))]
		//public async Task<IHttpActionResult> GetTeacherDepartment(int id)
		//{
		//	TeacherDepartment teacherdepartment = await db.TeacherDepartments.FindAsync(id);
		//	if (teacherdepartment == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(teacherdepartment);
		//}

		//// PUT api/TeacherDepartments/5
		//public async Task<IHttpActionResult> PutTeacherDepartment(int id, TeacherDepartment teacherdepartment)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != teacherdepartment.DepartmentId)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(teacherdepartment).State = EntityState.Modified;

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!TeacherDepartmentExists(id))
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

		//// POST api/TeacherDepartments
		//[ResponseType(typeof(TeacherDepartment))]
		//public async Task<IHttpActionResult> PostTeacherDepartment(TeacherDepartment teacherdepartment)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.TeacherDepartments.Add(teacherdepartment);
		//	await db.SaveChangesAsync();

		//	return CreatedAtRoute("DefaultApi", new { id = teacherdepartment.DepartmentId }, teacherdepartment);
		//}

		//// DELETE api/TeacherDepartments/5
		//[ResponseType(typeof(TeacherDepartment))]
		//public async Task<IHttpActionResult> DeleteTeacherDepartment(int id)
		//{
		//	TeacherDepartment teacherdepartment = await db.TeacherDepartments.FindAsync(id);
		//	if (teacherdepartment == null)
		//	{
		//		return NotFound();
		//	}

		//	db.TeacherDepartments.Remove(teacherdepartment);
		//	await db.SaveChangesAsync();

		//	return Ok(teacherdepartment);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		//private bool TeacherDepartmentExists(int id)
		//{
		//	return db.TeacherDepartments.Count(e => e.DepartmentId == id) > 0;
		//}
    }
}