using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CourseDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Teachers { get; set; }
		public List<CommentForCourseDTO> Comments { get; set; }

		public string Category { get; set; }

		public CourseDTO()
		{
			this.Teachers = new List<string>();
			this.Comments = new List<CommentForCourseDTO>();
		}
	}
}