using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public enum Assessment
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    };

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
        public Assessment Clarity { get; set; }
        public Assessment Workload { get; set; }
        public Assessment Interest { get; set; }
        public Assessment Simplicity { get; set; }
        public Assessment Usefulness { get; set; }
        public string Comment { get; set; }
    }

}