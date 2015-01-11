using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;
using FMIRatingsAPI.Statistics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FMIRatingsAPI.Controllers
{
    public class StatisticController : ApiController
    {
        private const String R_HOST = "localhost";
        private const int R_PORT = 8888;

        private readonly FMIRatingsContext _db = new FMIRatingsContext();

        private static String AsQueryString(Object o)
        {

            // TODO this is optimistic - need to make it something that at least converts arrays to how R expects them.
            return o.ToString();
        }

        private static Uri BuildExecutionUrl(String scriptName, Dictionary<String, Object> parameters)
        {
            NameValueCollection queries = System.Web.HttpUtility.ParseQueryString(String.Empty);

            foreach (var p in parameters)
            {
                queries[p.Key] = AsQueryString(p.Value);
            }

            UriBuilder builder = new UriBuilder();
            builder.Host = R_HOST;
            builder.Port = R_PORT;
            builder.Scheme = "http";
            builder.Path = scriptName;
            builder.Query = queries.ToString();

            return builder.Uri;
        }

        private static Uri BuildExecutionUrlEx(String scriptName, String data)
        {
            UriBuilder builder = new UriBuilder();
            builder.Host = R_HOST;
            builder.Port = R_PORT;
            builder.Scheme = "http";
            builder.Path = scriptName;
            builder.Query = String.Format("param={0}", HttpUtility.UrlEncode(data));

            return builder.Uri;
        }

        private Dictionary<String, Object> GetData(String transform, String input)
        {
            int inputId = System.Int32.Parse(input);
            InputTransformer transformer = new InputTransformer(transform, _db);
            return transformer.Transform(inputId);
        }

        private Dictionary<String, Object> GetDataEx(String targetType, String criterion)
        {
            Dictionary<String, Object> dict = new Dictionary<string,object>();

            Dictionary<int, IEnumerable<int>> assessments;
            Dictionary<int, String> names;

            int criterionId = int.Parse(criterion);
            // IQueryable<dynamic> table;
            switch (targetType)
            {
                case "Teacher":
                    {
                        IQueryable<VoteForTeacher> table = _db.VotesForTeachers.AsQueryable();

                        table = table.Where(v => v.CriterionId == criterionId);
                        assessments = table.OrderBy(v => v.TeacherId).ToLookup(v => v.TeacherId).ToDictionary(x => x.Key, x => x.ToArray().Select(z => z.Assessment));
                        names = _db.Teachers.ToDictionary(t => t.Id, t => t.Name);

                        break;
                    }
                case "Course":
                    {
                        IQueryable<VoteForCourse> table = _db.VotesForCourses.AsQueryable();

                        table = table.Where(v => v.CriterionId == criterionId);
                        assessments = table.OrderBy(v => v.CourseId).ToLookup(v => v.CourseId).ToDictionary(x => x.Key, x => x.ToArray().Select(z => z.Assessment));
                        names = _db.Courses.ToDictionary(c => c.Id, c => c.Name);

                        break;
                    }
                default:
                    throw new ArgumentException();
            }

            dict["assessment"] = assessments;
            dict["names"] = names;

            return dict;
        }

        [HttpGet]
        public HttpResponseMessage Execute(int id, String input)
        {
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Redirect);

            Statistic stat = _db.Statistics.Where(s => s.Id == id).SingleOrDefault();
            //Statistic stat = new Statistic() { Name = "name" };
            Dictionary<String, Object> scriptInput = GetData(stat.InputTransform, input);

            Uri url = BuildExecutionUrl(stat.Name, scriptInput);

            result.Headers.Location = url;

            return result;
        }

        [HttpGet]
        public List<StatisticDTO> Query(String objectType)
        {
            return _db.Statistics.
                Where(s => s.InputType.Equals(objectType, StringComparison.InvariantCultureIgnoreCase)).
                Select(t => new StatisticDTO(){ Id = t.Id, Name = t.Name}).
                ToList();
        }

        [HttpGet]
        public List<ExtremumDTO> ExecuteMaxMin(String targetType)
        {
            List<ExtremumDTO> result = new List<ExtremumDTO>();

            if (targetType == "Course")
            {
                foreach (CriterionForCourse cfc in _db.CriteriaForCourses)
                {
                    ObjectScore minScore;
                    ObjectScore maxScore;
                    double average;

                    var votes = _db.VotesForCourses.Where(v => v.CriterionId == cfc.Id);
                    var min = votes.First(v => v.Assessment == votes.Min(x => x.Assessment));
                    var max = votes.First(v => v.Assessment == votes.Max(x => x.Assessment));
                    average = votes.Average(v => v.Assessment);

                    minScore = new ObjectScore() { Score = min.Assessment, Target = min.Course.Name, TargetId = min.CourseId};
                    maxScore = new ObjectScore() { Score = max.Assessment, Target = max.Course.Name, TargetId = max.CourseId };

                    result.Add(new ExtremumDTO() {
                        Avg = average,
                        Max = maxScore,
                        Min = minScore,
                        CriterionId = cfc.Id,
                        CriterionName = cfc.Name
                    });
                }
            }
            else if (targetType == "Teacher")
            {
                foreach (CriterionForTeacher cft in _db.CriteriaForTeachers)
                {
                    ObjectScore minScore;
                    ObjectScore maxScore;
                    double average;

                    var votes = _db.VotesForTeachers.Where(v => v.CriterionId == cft.Id);
                    var min = votes.First(v => v.Assessment == votes.Min(x => x.Assessment));
                    var max = votes.First(v => v.Assessment == votes.Max(x => x.Assessment));
                    average = votes.Average(v => v.Assessment);

                    minScore = new ObjectScore() { Score = min.Assessment, Target = min.Teacher.Name, TargetId = min.TeacherId };
                    maxScore = new ObjectScore() { Score = max.Assessment, Target = max.Teacher.Name, TargetId = max.TeacherId };

                    result.Add(new ExtremumDTO()
                    {
                        Avg = average,
                        Max = maxScore,
                        Min = minScore,
                        CriterionId = cft.Id,
                        CriterionName = cft.Name
                    });
                }
            }
            else
            {
                throw new ArgumentException("fira");
            }

            return result;
        }

        [HttpGet]
        public HttpResponseMessage ExecuteImg(String targetType, String criterionId, String scriptName)
        {
            return ExecuteImg(targetType, null, criterionId, scriptName);
        }

        [HttpGet]
        public HttpResponseMessage ExecuteImg(String targetType, String input, String criterionId, String scriptName)
        {
            Dictionary<String, Object> data = GetDataEx(targetType, criterionId);

            String str = JsonConvert.SerializeObject(data);

            Uri executionUri = BuildExecutionUrl(scriptName, data);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Redirect);
            result.Headers.Location = executionUri;

            return result;
        }
    }
}
