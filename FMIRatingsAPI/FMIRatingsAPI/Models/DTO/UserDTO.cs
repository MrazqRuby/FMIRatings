using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
		public string Name { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }
        public int GraduationYear { get; set; }

        public UserDTO (User user)
        {
            Id = user.Id;
            Name = user.Name;
            RealName = user.RealName;
            Email = user.Email;
            Major = user.Major;
            Course = user.Course;
            Group = user.Group;
            GraduationYear = DateTime.Now.Year + 5 - user.Course;
        }
    }

    public class UserWithPasswordDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string RealName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Course { get; set; }

        public int Group { get; set; }

        public int GraduationYear { get; set; }

        [Required]
        public string Major { get; set; } 
    }
}