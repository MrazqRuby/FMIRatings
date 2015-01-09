using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CommentDTO
	{
		public int Id { get; set; }
        public string Author{ get; set; }
		public string Text { get; set; }
		public DateTime DateCreated { get; set; }
	}
}