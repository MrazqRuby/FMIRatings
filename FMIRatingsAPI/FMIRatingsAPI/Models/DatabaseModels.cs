using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMIRatingsAPI.Models
{
	//public class User
	//{
	//	[Key]
	//	public int Id { get; set; }

	//	[StringLength(256)]
	//	[Required]
	//	[Index("IX_UniqueUserName", IsUnique = true)]
	//	public string Name { get; set; }

	//	[Required]
	//	public string Password { get; set; }
	//}

	//public class Discipline
	//{ 
	//	[Key]
	//	public int Id { get; set;  }
	//	[Required]
	//	[StringLength(256)]
	//	[Index("IX_UniqueDisciplineName", IsUnique = true)]
	//	public string Name { get; set; }
	//	public string Description { get; set; }

	//	public virtual List<Teacher> Tutors { get; set; }

	//	public Discipline()
	//	{
	//		Tutors = new List<Teacher>();
	//	}
	//}

	//public class Teacher
	//{
	//	[Key]
	//	public int Id { get; set; }
	//	[Required]
	//	[StringLength(256)]
	//	[Index("IX_UniqueTeacherName", IsUnique = true)]
	//	public string Name { get; set; }

	//	public virtual List<Discipline> CoursesTaught { get; set; }

	//	public Teacher()
	//	{
	//		CoursesTaught = new List<Discipline>();
	//	}
	//}
}
