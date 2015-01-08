using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
    public class VoteForTeacher
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public int CriterionId { get; set; }
        public Assessment Assesment { get; set; }

        public Teacher Teacher { get; set; }
        public CriterionForTeacher Criterion { get; set; }
    }
}