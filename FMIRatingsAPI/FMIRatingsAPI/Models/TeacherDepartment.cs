using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class TeacherDepartment
	{
		[Key]
		public int DepartmentId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Teacher> Teachers { get; set; }
	}
}