using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class Teacher
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string Name { get; set; }

		public int DepartmentId { get; set; }
		
		[ForeignKey("DepartmentId")]

		public virtual TeacherDepartment Department { get; set; }

		public virtual ICollection<TeacherInCourse> Courses { get; set; }
		public virtual ICollection<CommentForTeacher> Comments { get; set; }
	}
}