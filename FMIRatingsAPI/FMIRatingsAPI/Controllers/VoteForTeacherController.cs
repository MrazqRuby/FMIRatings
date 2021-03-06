﻿using System;
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
    public class VoteForTeacherController : ApiController
    {
        private FMIRatingsContext db = new FMIRatingsContext();

        // GET api/VoteForCourse
        /// <summary>
        /// Get all votes and statistics for the avarage assessment per criterion for teachers
        /// </summary>
        /// <returns>List of votes for all teachers</returns>
        public List<VoteForTeacherDTO> GetVotesForTeachers()
        {
            var allTeachers = db.Teachers.Select(x => x).Distinct();
            var result = new List<VoteForTeacherDTO>();

            foreach (var teacher in allTeachers)
            {
                var groupsQuery =
                from c in db.VotesForTeachers.Where(t => t.TeacherId== teacher.Id)
                group c by c.CriterionId into newGroup
                orderby newGroup.Key
                select newGroup;

               var votesForCourse = new VoteForTeacherDTO()
               {
                   TeacherId = teacher.Id,
                   TeacherName = db.Teachers.Where(x => x.Id == teacher.Id).Select(t => t.Name).FirstOrDefault(),
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
        /// Get all votes and statistics for avarage assessment per criterion for a current teacher
        /// </summary>
        /// <param name="id">id of the teacher</param>
        /// <returns>Vote for current teacher</returns>
        [ResponseType(typeof(VoteForTeacherDTO))]
        public async Task<IHttpActionResult> GetVotesForTeacher(int id)
        {
 
            var groupsQuery = 
                from teacher in db.VotesForTeachers.Where(t => t.TeacherId == id)
                group teacher by teacher.CriterionId into newGroup
                orderby newGroup.Key
                select newGroup;

            var votesForTeacher = new VoteForTeacherDTO();

            await Task.Run(() =>
            {
                votesForTeacher = new VoteForTeacherDTO()
                    {
                        TeacherId = id,
                        TeacherName = db.Teachers.Where(x => x.Id == id).Select(t => t.Name).FirstOrDefault(),
                        Votes = groupsQuery.Select(group => new AvarageDTO()
                        {
                            CriterionId = group.Key,
                            CriterionName = group.Select(x => x.Criterion.Name).FirstOrDefault(),
                            Avarage = group.Average(a => a.Assessment)
                        }).ToList<AvarageDTO>()
                    };
            });

            if (votesForTeacher == null)
            {
                return NotFound();
            }

            return Ok(votesForTeacher);
        }



        // POST api/VoteForCourse
        /// <summary>
        /// Post a vote for current teacher
        /// </summary>
        /// <param name="voteForTeacher">object which containt teacherId, Assessments (from 1 to 5) for Clarity, Enthusiasm, Speed, Scope and Evaluation</param>
        /// <returns>Status Code 200 if succeed</returns>
        [AuthenticationFilter]
        public IHttpActionResult PostVoteForTeacher([FromBody] BrowserVoteForTeacherDTO voteForTeacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Teacher teacher = db.Teachers.Where(c => c.Id == voteForTeacher.TeacherId).FirstOrDefault();

            // Запазваме ID на критериите
            int ClarityID = db.CriteriaForTeachers.First(c => c.Name == "Яснота").Id;
            int EnthusiasmID = db.CriteriaForTeachers.First(c => c.Name == "Ентусиазъм").Id;
            int EvaluationID = db.CriteriaForTeachers.First(c => c.Name == "Критерии на оценяване").Id;
            int SpeedID = db.CriteriaForTeachers.First(c => c.Name == "Скорост на преподаване").Id;
            int ScopeID = db.CriteriaForTeachers.First(c => c.Name == "Обхват на преподавания материал").Id;

            //Дали потребителя е гласувал за този учител по параметър
            int userId = UserManager.GetCurrentUser().Id;
            if (VoteForTeacherExists(voteForTeacher.TeacherId, ClarityID, userId) ||
                VoteForTeacherExists(voteForTeacher.TeacherId, EnthusiasmID, userId) ||
                VoteForTeacherExists(voteForTeacher.TeacherId, EvaluationID, userId) ||
                VoteForTeacherExists(voteForTeacher.TeacherId, SpeedID, userId) ||
                VoteForTeacherExists(voteForTeacher.TeacherId, ScopeID, userId))
            {
                return Conflict();
            }

            if (teacher != null)
            {
                db.VotesForTeachers.AddRange(new VoteForTeacher[] {
                    new VoteForTeacher() 
                    { 
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        CriterionId = ClarityID,
                        Assessment = (int)voteForTeacher.Clarity,
                    },
                    new VoteForTeacher() 
                    { 
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        CriterionId = EnthusiasmID,
                        Assessment = (int)voteForTeacher.Enthusiasm,
                    },
                    new VoteForTeacher() 
                    { 
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        CriterionId = EvaluationID,
                        Assessment = (int)voteForTeacher.Speed,
                    },
                    new VoteForTeacher() 
                    { 
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        CriterionId = SpeedID,
                        Assessment = (int)voteForTeacher.Scope,
                    },
                    new VoteForTeacher() 
                    { 
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        CriterionId = ScopeID,
                        Assessment = (int)voteForTeacher.Evaluation,
                    },
                });
                db.CommentsForTeachers.Add(
                    new CommentForTeacher()
                    {
                        UserId = UserManager.GetCurrentUser().Id,
                        TeacherId = voteForTeacher.TeacherId,
                        Text = voteForTeacher.Comment,
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

        private bool VoteForTeacherExists(int teacherId, int criterionId, int userId)
        {
            return db.VotesForTeachers.Count(e => 
                e.TeacherId == teacherId && 
                e.CriterionId == criterionId &&
                e.UserId == userId ) > 0;
        }
    }
}