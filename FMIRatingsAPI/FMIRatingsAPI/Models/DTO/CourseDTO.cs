﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CourseDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Teachers { get; set; }

		public CourseDTO()
		{
			this.Teachers = new List<string>();
		}
	}
}