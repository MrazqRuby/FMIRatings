using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class CommentForTeacher : Comment
	{
		//[Key]
		//public int Id { get; set;  }
		public int TeacherId { get; set; }
		//TODO: add a reference to the User
		//public int? AuthorId { get; set; }
		//public string Text { get; set; }
		//public DateTime DateCreated { get; set; }

	}
}