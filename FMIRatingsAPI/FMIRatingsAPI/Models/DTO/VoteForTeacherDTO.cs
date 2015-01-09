using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class VoteForTeacherDTO
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public List<AvarageDTO> Votes { get; set; }

        public VoteForTeacherDTO()
        {
            this.Votes = new List<AvarageDTO>();
        }
    }

    public class BrowserVoteForTeacherDTO
    {
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public Assessment Clarity { get; set; }
        public Assessment Enthusiasm { get; set; }
        public Assessment Evaluation { get; set; }
        public Assessment Speed { get; set; }
        public Assessment Scope { get; set; }
        public string Comment { get; set; }
    }
}