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
        [Key]
        public int Id { get; set; }
        [Index("IX_Name", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        [Index("IX_Email", 1, IsUnique = true)]
        [MaxLength(450)]
        public string Email { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }
        public string Major { get; set; } 

        public virtual ICollection<CommentForTeacher> TeacherComments { get; set; }
        public virtual ICollection<CommentForCourse> CourseComments { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}