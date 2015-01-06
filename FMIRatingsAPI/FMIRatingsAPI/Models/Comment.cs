using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }

		//TODO: add a reference to the User
		public int? AuthorId { get; set; }
		public string Text { get; set; }
		public DateTime DateCreated { get; set; }
	}
}