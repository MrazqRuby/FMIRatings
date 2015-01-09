using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CommentForTeacherDTO : CommentDTO
	{
		public int TeacherId { get; set; }
	}
}