using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api/files")]
    public class FileController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        [HttpGet]
        [Route("forcourse/{courseId}")]
        public IHttpActionResult GetFilesForCourse(int courseId)
        {
            FileForCourseDTO[] files = db.Files.Where(f => f.CourseId == courseId).Select(f => new FileForCourseDTO {Id = f.Id, Filename = f.Filename, CourseId = f.CourseId}).ToArray();
           
            return Ok(files);
        }
    }
}