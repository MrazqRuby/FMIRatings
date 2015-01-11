using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class ObjectScore
    {
        public double Score { get; set;}
        public String Target { get; set; }
        public int TargetId { get; set; }
    }

    public class ExtremumDTO
    {
        public int CriterionId { get; set; }
        public String CriterionName { get; set; }

        public ObjectScore Min { get; set; }
        public ObjectScore Max { get; set; }
        public double Avg { get; set; }
    }
}