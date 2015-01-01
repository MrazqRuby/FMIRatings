﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;

namespace FMIRatingsAPI.Controllers
{
    public class TeachersController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/Teachers
        public List<TeacherDTO> GetTeachers()
        {
			var teachers = db.Teachers.Select(teacher =>
				new TeacherDTO()
				{
					Name = teacher.Name,
					Courses = teacher.Courses.Select(course => 
						course.Course.Name).ToList<string>()
				}).ToList();

			return teachers;
        }

		//// GET api/Teachers/5
		//[ResponseType(typeof(Teacher))]
		//public IHttpActionResult GetTeacher(int id)
		//{
		//	Teacher teacher = db.Teachers.Find(id);
		//	if (teacher == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(teacher);
		//}

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