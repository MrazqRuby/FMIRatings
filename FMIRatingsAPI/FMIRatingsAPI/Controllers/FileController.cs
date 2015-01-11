using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api/files")]
    public class FileController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        [HttpGet]
        [Route("forcourse/{courseId}")]
        [ResponseType(typeof(FileForCourseDTO[]))]
        public IHttpActionResult GetFilesForCourse(int courseId)
        {
            FileForCourseDTO[] files = db.Files.Where(f => f.CourseId == courseId).Select(f => new FileForCourseDTO {Id = f.Id, Filename = f.Filename, CourseId = f.CourseId}).ToArray();
           
            return Ok(files);
        }

        [HttpGet]
        [Route("{fileId}")]
        public HttpResponseMessage GetFile(int fileId)
        {
            HttpResponseMessage result = null;
            var entry = db.Files.SingleOrDefault(f => f.Id == fileId);
            if (entry == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            var localFilePath = entry.Path;
            
            if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {// serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = entry.Filename;                
            }

            return result;
        }
    }
}