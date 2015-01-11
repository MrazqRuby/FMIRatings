using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models.DTO
{
    public class FileForCourseDTO
    {
        public string Filename { get; set; }
        public int CourseId { get; set; }
    }
}