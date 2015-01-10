using FMIRatingsAPI.Authentication;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace FMIRatingsAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("postuser")]
        public IHttpActionResult PostUser([FromBody]UserWithPasswordDTO user)
        {
            UserDTO createdUser;
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
            return Ok(createdUser);
        }

        [HttpPost]
        [Route("getauthtoken")]
        public IHttpActionResult GetAuthToken([FromBody] UserWithPasswordDTO user)
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
    }
}