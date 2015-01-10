using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models.DTO;
using FMIRatingsAPI.Authentication;

namespace FMIRatingsAPI.Controllers
{
    public class VoteForCourseController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/VoteForCourse
        /// <summary>
        /// Get all votes and statistics for the avarage assessment per criterion for courses
        /// </summary>
        /// <returns>List of votes for all courses</returns>
        public List<VoteForCourseDTO> GetVotesForCourses()
        {
            var allCourses = db.Courses.Select(x => x).Distinct();
            var result = new List<VoteForCourseDTO>();

            foreach (var course in allCourses)
            {
                var groupsQuery =
                from c in db.VotesForCourses.Where(t => t.CourseId == course.Id)
                group c by c.CriterionId into newGroup
                orderby newGroup.Key
                select newGroup;

               var votesForCourse = new VoteForCourseDTO()
               {
                   CourseId = course.Id,
                   CourseName = db.Courses.Where(x => x.Id == course.Id).Select(t => t.Name).FirstOrDefault(),
                   Votes = groupsQuery.Select(group => new AvarageDTO()
                   {
                       CriterionId = group.Key,
                       CriterionName = group.Select(x => x.Criterion.Name).FirstOrDefault(),
                       Avarage = group.Average(a => (int)a.Assessment)
                   }).ToList<AvarageDTO>()
               };

               result.Add(votesForCourse);
            }


            return result;
        }

        // GET api/VoteForCourse/5
        //return all votes for current course
        /// <summary>
        /// Get all votes and statistics for avarage assessment per criterion for a current course
        /// </summary>
        /// <param name="id">id of the course</param>
        /// <returns>Vote for current course</returns>
        [ResponseType(typeof(VoteForCourseDTO))]
        public async Task<IHttpActionResult> GetVotesForCourse(int id)
        {
 
            var groupsQuery = 
                from course in db.VotesForCourses.Where(t => t.CourseId == id)
                group course by course.CriterionId into newGroup
                orderby newGroup.Key
                select newGroup;

            var votesForCourse = new VoteForCourseDTO();

            await Task.Run(() =>
            {
                votesForCourse = new VoteForCourseDTO()
                    {
                        CourseId = id,
                        CourseName = db.Courses.Where(x => x.Id == id).Select(t => t.Name).FirstOrDefault(),
                        Votes = groupsQuery.Select(group => new AvarageDTO()
                        {
                            CriterionId = group.Key,
                            CriterionName = group.Select(x => x.Criterion.Name).FirstOrDefault(),
                            Avarage = group.Average(a => a.Assessment)
                        }).ToList<AvarageDTO>()
                    };
            });

            if (votesForCourse == null)
            {
                return NotFound();
            }

            return Ok(votesForCourse);
        }



        // POST api/VoteForCourse
        /// <summary>
        /// Post a vote for current course
        /// </summary>
        /// <param name="voteForTeacher">object which containt teacherId, Assessments (from 1 to 5) for Clarity, Workload, Interest, Simplicity and Usefulness</param>
        /// <returns>Status Code 200 if succeed</returns>
        public IHttpActionResult PostVoteForCourse([FromBody] BrowserVoteForCourseDTO voteForCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Course course = db.Courses.Where(c => c.Id == voteForCourse.CourseId).FirstOrDefault();

            // Запазваме ID на критериите
            int ClarityID = db.CriteriaForCourses.First(c => c.Name == "Полезност").Id;
            int WorkloadID = db.CriteriaForCourses.First(c => c.Name == "Яснота").Id;
            int UsefulnessID = db.CriteriaForCourses.First(c => c.Name == "Интерес").Id;
            int SimplicityID = db.CriteriaForCourses.First(c => c.Name == "Натовареност").Id;
            int InterestID = db.CriteriaForCourses.First(c => c.Name == "Леснота").Id;

            //Дали потребителя е гласувал за този курс по параметър
            int userId = UserManager.GetCurrentUser().Id;
            if (VoteForCourseExists(voteForCourse.CourseId, ClarityID, userId) ||
                VoteForCourseExists(voteForCourse.CourseId, WorkloadID, userId) ||
                VoteForCourseExists(voteForCourse.CourseId, UsefulnessID, userId) ||
                VoteForCourseExists(voteForCourse.CourseId, SimplicityID, userId) ||
                VoteForCourseExists(voteForCourse.CourseId, InterestID, userId))
            {
                return Conflict();
            }

            if (course != null)
            {
                db.VotesForCourses.AddRange(new VoteForCourse[] {
                    new VoteForCourse() 
                    { 
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        CriterionId = ClarityID,
                        Assessment = (int)voteForCourse.Clarity,
                    },
                    new VoteForCourse() 
                    { 
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        CriterionId = WorkloadID,
                        Assessment = (int)voteForCourse.Workload,
                    },
                    new VoteForCourse() 
                    { 
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        CriterionId = UsefulnessID,
                        Assessment = (int)voteForCourse.Usefulness,
                    },
                    new VoteForCourse() 
                    { 
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        CriterionId = SimplicityID,
                        Assessment = (int)voteForCourse.Simplicity,
                    },
                    new VoteForCourse() 
                    { 
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        CriterionId = InterestID,
                        Assessment = (int)voteForCourse.Interest,
                    },
                });
                db.CommentsForCourses.Add(
                    new CommentForCourse()
                    {
                        UserId = 1,
                        CourseId = voteForCourse.CourseId,
                        Text = voteForCourse.Comment,
                        DateCreated = DateTime.Now,
                    });
            }
            db.SaveChanges();
            
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoteForCourseExists(int courseId, int criterionId, int userId)
        {
            return db.VotesForCourses.Count(e => 
                e.CourseId == courseId && 
                e.CriterionId == criterionId &&
                e.UserId == userId ) > 0;
        }
    }
}