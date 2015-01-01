using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class TeacherDTO
	{
		public string Name { get; set; }

		public List<string> Courses { get; set; }

		public TeacherDTO()
		{
			this.Courses = new List<string>();
		}
	}
}