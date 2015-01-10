using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FMIRatingsAPI.Models.DTO;

namespace FMIRatingsAPI.Models
{
	public class Course
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set;  }
        public string Name { get; set; }
        public string Description { get; set; }

		public int? CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public virtual CourseCategory Category{ get; set; }

		public virtual ICollection<TeacherInCourse> Teachers { get; set; }
		public virtual ICollection<CommentForCourse> Comments { get; set; }
	}
}