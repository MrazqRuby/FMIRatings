using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class TeacherInCourse
	{
		[Key]
		public int Id { get; set; }
		public int TeacherId { get; set; }
		public int CourseId { get; set; } 

		public Course Course { get; set; }
		public Teacher Teacher { get; set; }
	}
}