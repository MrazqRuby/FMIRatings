using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMIRatingsAPI.Models
{
	public class CommentForCourse : Comment
	{
		public int CourseId { get; set; }
	}
}
