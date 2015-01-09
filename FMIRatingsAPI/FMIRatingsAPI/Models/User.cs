using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<CommentForTeacher> TeacherComments { get; set; }
        public virtual ICollection<CommentForCourse> CourseComments { get; set; }
    }
}