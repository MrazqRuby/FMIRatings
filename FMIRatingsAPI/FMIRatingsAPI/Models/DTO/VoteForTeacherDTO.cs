using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class VoteForTeacherDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CriterionId { get; set; }
        public Assessment Assessment { get; set; }

        public List<AvarageDTO> Votes { get; set; }

        public VoteForTeacherDTO()
        {
            this.Votes = new List<AvarageDTO>();
        }
    }
}