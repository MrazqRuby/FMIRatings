using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
		public string Name { get; set; }
    }

    public class UserWithPasswordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}