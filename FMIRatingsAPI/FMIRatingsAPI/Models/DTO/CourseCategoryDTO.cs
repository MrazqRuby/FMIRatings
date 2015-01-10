using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CourseCategoryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<CourseDTO> Courses { get; set; }
	}
}