using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class Course
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set;  }
        public string Name { get; set; }
        public string Description { get; set; }

		public virtual ICollection<TeacherInCourse> Teachers { get; set; }
	}
}