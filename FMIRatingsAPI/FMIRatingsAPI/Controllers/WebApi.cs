using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FMIRatingsAPI.Models;
using Newtonsoft.Json;

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api")]
    public class WebApiController : ApiController
    {
		private DatabaseContext db = new DatabaseContext();

		// GET api/Account
		[Route("Account")]
		public IEnumerable<User> GetUsers()
		{
			return db.Users.AsEnumerable();
		}

		// GET api/Account/5
		[Route("Account/{id}")]
		public User GetUser(int id)
		{
			User user = db.Users.Find(id);
			if (user == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return user;
		}

		// PUT api/Account/5
		[Route("Account/{id}")]
		public HttpResponseMessage PutUser(int id, User user)
		{
			if (ModelState.IsValid && id == user.Id)
			{
				db.Entry(user).State = EntityState.Modified;

				try
				{
					db.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				return Request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// POST api/Account
		[Route("Account")]
		public HttpResponseMessage PostUser(User user)
		{
			if (ModelState.IsValid)
			{
				db.Users.Add(user);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// DELETE api/Account/5
		[Route("Account/{id}")]
		public HttpResponseMessage DeleteUser(int id)
		{
			User user = db.Users.Find(id);
			if (user == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Users.Remove(user);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, user);
		}

		[Route("Disciplines")]
		public IHttpActionResult GetDisciplines()
		{
			var disciplines = FMIRatingsAPI.WebApiModels.ModelFactory.GetDisciplines();
			return Ok(disciplines);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
    }
}