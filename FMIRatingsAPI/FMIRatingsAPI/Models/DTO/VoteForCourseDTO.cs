using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class VoteForCourseDTO
    {

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public List<AvarageDTO> Votes { get; set; }

        public VoteForCourseDTO()
        {
            this.Votes = new List<AvarageDTO>();
        }

      
    }

    public class BrowserVoteForCourseDTO
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int Clarity { get; set; }
        public int Workload { get; set; }
        public int Interest { get; set; }
        public int Simplicity { get; set; }
        public int Usefulness { get; set; }
        public string Comment { get; set; }
        public Assessment Assessment { get; set; }
    }

}