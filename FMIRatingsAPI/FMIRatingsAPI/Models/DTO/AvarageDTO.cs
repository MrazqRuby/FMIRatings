using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class AvarageDTO
    {
        public int CriterionId { get; set; }
        public string CriterionName { get; set; }
        public double Avarage { get; set; }
    }
}