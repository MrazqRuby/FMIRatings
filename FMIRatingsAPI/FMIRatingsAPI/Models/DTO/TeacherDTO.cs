using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class TeacherDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Department { get; set; }

		public List<CourseDTO> Courses { get; set; }

		public List<CommentForTeacherDTO> Comments { get; set; }
		public TeacherDTO()
		{
			this.Courses = new List<CourseDTO>();
			this.Comments = new List<CommentForTeacherDTO>();
		}
	}
}