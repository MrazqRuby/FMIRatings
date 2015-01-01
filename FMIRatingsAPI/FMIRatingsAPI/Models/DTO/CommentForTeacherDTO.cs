using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
	public class CommentForTeacherDTO
	{
		//public int Id { get; set; }

		private string _author;
		//TODO: Update info when Users are available
		public string Author
		{
			get
			{
				if (!String.IsNullOrEmpty(this._author))
				{
					this._author = "Stamo";
				}
				return this._author;
			}
			set
			{
				this._author = value;
			}
		}
		public string Text { get; set; }
		public DateTime DateCreated { get; set; }
	}
}