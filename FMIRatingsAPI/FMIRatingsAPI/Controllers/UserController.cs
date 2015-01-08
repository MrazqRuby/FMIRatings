using FMIRatingsAPI.Authentication;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace FMIRatingsAPI.Controllers
{
    public class UsersController : ApiController
    {
        [ResponseType(typeof(TeacherDTO))]
        public IHttpActionResult PostUser([FromBody]UserWithPasswordDTO user)
        {
            UserDTO createdUser = UserManager.CreateUser(user);
            if ( createdUser == null)
                return Conflict();
            return Ok(createdUser);
        }
    }
}