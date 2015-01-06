using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CommentForCourseDTO : CommentDTO
	{
		public int CourseId { get; set; }
	}
}