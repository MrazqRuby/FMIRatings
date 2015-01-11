using FMIRatingsAPI.Authentication;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using FMIRatingsAPI.DAL;

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        /// <summary>
        /// This method creates a user with the supplied credentials on the server,
        /// or returns 409 Conflict if a user with those credentials exists
        /// </summary>
        /// <param name="user">The credentials of the user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("postuser")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult PostUser([FromBody]UserWithPasswordDTO user)
        {
            User createdUser;
            try
            {
                createdUser = UserManager.CreateUser(user);
                if (createdUser == null)
                    return Conflict();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
            return Ok(new UserDTO(createdUser));
        }

        /// <summary>
        /// Method for making authentication token
        /// </summary>
        /// <param name="user">User's data</param>
        /// <returns>The token</returns>
        [HttpPost]
        [Route("getauthtoken")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetAuthToken([FromBody] SimpleUserDTO user)
        {
            if (UserManager.GetUser(user.Name) == null)
            {
                return Unauthorized();
            }
            string token;
            byte[] bytes = Encoding.UTF8.GetBytes(user.Name + ":" + user.Password);
            token = Convert.ToBase64String(bytes);
            return Ok(token);
        }

        /// <summary>
        /// Method for making authentication token, which authorize the user by Base64Encode
        /// </summary>
        /// <param name="user">User's data</param>
        /// <returns>The token</returns>
        [HttpPost]
        [Route("upload")]
        [AuthenticationFilter]
        public async Task<IHttpActionResult> UploadFile()
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                int courseId = 0;
                foreach (string key in provider.FormData.AllKeys)
	            {
                    if (key.Equals("CourseId", StringComparison.CurrentCultureIgnoreCase))
                    {
                        courseId = int.Parse(provider.FormData[key]);
                    }
	            } 

                foreach (MultipartFileData file in provider.FileData)
                {
                    File newUpload = new File
                    {
                        CourseId = courseId,
                        Path = file.LocalFileName,
                        Filename = file.Headers.ContentDisposition.FileName,
                        UserId = UserManager.GetCurrentUser().Id
                    };
                    db.Files.Add(newUpload);
                }
                db.SaveChanges();
                return Ok();
            }
            catch (System.Exception e)
            {
                return Ok(e.Message);
            }


        }

        [AuthenticationFilter]
        public IHttpActionResult GetUser()
        {
            return Ok(new UserDTO(UserManager.GetCurrentUser()));
        }
    }
}