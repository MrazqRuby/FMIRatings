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

namespace FMIRatingsAPI.Controllers
{
    public class VoteForCourseController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/VoteForCourse
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
        [ResponseType(typeof(VoteForCourseDTO))]
        public async Task<IHttpActionResult> PostVoteForCourse([FromBody] VoteForCourseDTO voteForCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VoteForCourse course = db.VotesForCourses.Where(c => c.Course.Id == voteForCourse.CourseId).FirstOrDefault();
            if (course != null)
            {
                db.VotesForCourses.Add(new VoteForCourse() {
                    UserId = 88,
                    CourseId = voteForCourse.CourseId,
                    CriterionId = voteForCourse.CriterionId,
                    Assessment = (int)voteForCourse.Assessment,
                });
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //Дали потребителя е гласувал за този курс по параметър
                if (VoteForCourseExists(voteForCourse.CourseId, voteForCourse.CriterionId, voteForCourse.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = voteForCourse.Id }, voteForCourse);
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