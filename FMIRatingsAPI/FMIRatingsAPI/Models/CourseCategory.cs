using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class CourseCategory
	{
		[Key]
		public int CategoryId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Course> Courses { get; set; }
	}
}