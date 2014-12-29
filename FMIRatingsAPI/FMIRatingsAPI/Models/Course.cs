using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class Course
	{
		[Key]
        public int Id { get; set;  }
        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
		
	}
}