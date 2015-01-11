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

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
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
                return Ok(exc.Message);
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
        [ResponseType(typeof(string))]
        [AuthenticationFilter]
        public IHttpActionResult UploadFile()
        {
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (string key in provider.FormData.AllKeys)
	            {
                    Trace.WriteLine(key);      
	            } 

                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Ok();
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

        [AuthenticationFilter]
        public IHttpActionResult GetUser()
        {
            return Ok(new UserDTO(UserManager.GetCurrentUser()));
        }
    }
}