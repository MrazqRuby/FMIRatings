using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class VoteForCourseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CriterionId { get; set; }
        public Assessment Assessment { get; set; }

        public List<AvarageDTO> Votes { get; set; }

        public VoteForCourseDTO()
        {
            this.Votes = new List<AvarageDTO>();
        }

      
    }
}