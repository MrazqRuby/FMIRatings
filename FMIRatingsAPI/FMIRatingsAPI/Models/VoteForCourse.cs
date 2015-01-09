using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
    public class VoteForCourse
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int CriterionId { get; set; }
        public int Assessment { get; set; }

        public virtual Course Course { get; set; }
        public virtual CriterionForCourse Criterion { get; set; }
    }
}