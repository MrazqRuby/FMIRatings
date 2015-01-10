using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIRatingsAPI.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public User user;
        public Course course;
    }
}