using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
    public class Statistic
    {
        public enum ResultType
        {
            DATA,
            IMAGE
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public String Name { get; set; }
        public String FileName { get; set; }
        public String InputType { get; set; }
        public ResultType ReturnType { get; set; }
        public String InputTransform { get; set; }
    }
}