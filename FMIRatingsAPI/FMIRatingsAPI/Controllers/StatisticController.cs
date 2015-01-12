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


                    var voteGroupsAsc = _db.VotesForCourses.
                        Where(v => v.CriterionId == cfc.Id).
                        GroupBy(v => v.CourseId).
                        OrderBy(g => g.Average(h => h.Assessment));
                    var voteGroupsDesc = _db.VotesForCourses.
                        Where(v => v.CriterionId == cfc.Id).
                        GroupBy(v => v.CourseId).
                        OrderByDescending(g => g.Average(h => h.Assessment));
                    var min = voteGroupsAsc.First();
                    var max = voteGroupsDesc.First();
                    average = voteGroupsAsc.Average(v => v.Average(s => s.Assessment));

                    minScore = new ObjectScore()
                    {
                        Score = min.Average(m => m.Assessment),
                        Target = min.ElementAt(0).Course.Name,
                        TargetId = min.ElementAt(0).CourseId
                    };
                    maxScore = new ObjectScore()
                    {
                        Score = max.Average(m => m.Assessment),
                        Target = max.ElementAt(0).Course.Name,
                        TargetId = max.ElementAt(0).CourseId
                    };

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

                    var voteGroupsAsc = _db.VotesForTeachers.
                        Where(v => v.CriterionId == cft.Id).
                        GroupBy(v => v.TeacherId).
                        OrderBy(g => g.Average(h => h.Assessment));
                    var voteGroupsDesc = _db.VotesForTeachers.
                        Where(v => v.CriterionId == cft.Id).
                        GroupBy(v => v.TeacherId).
                        OrderByDescending(g => g.Average(h => h.Assessment));
                    var min = voteGroupsAsc.First();
                    var max = voteGroupsDesc.First();
                    average = voteGroupsAsc.Average(v => v.Average(s => s.Assessment));

                    minScore = new ObjectScore() {
                        Score = min.Average(m => m.Assessment),
                        Target = min.ElementAt(0).Teacher.Name,
                        TargetId = min.ElementAt(0).TeacherId };
                    maxScore = new ObjectScore() {
                        Score = max.Average(m => m.Assessment),
                        Target = max.ElementAt(0).Teacher.Name,
                        TargetId = max.ElementAt(0).TeacherId };

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
        public HttpResponseMessage TeachersByCriterion(String criterionId)
        {
            int criterion = int.Parse(criterionId);
            var teacherScores = _db.VotesForTeachers.
                        Where(v => v.CriterionId == criterion).
                        OrderBy(v => v.TeacherId).
                        GroupBy(v => v.TeacherId).
                        Select(v => v.Average(c => c.Assessment));

            var teacherNames = _db.Teachers.OrderBy(t => t.Id).Select(t => t.Name);

            String names = JsonConvert.SerializeObject(teacherNames);
            String scores = JsonConvert.SerializeObject(teacherScores);

            Dictionary<String, Object> dict = new Dictionary<string,object>();
            dict["teacherNames"] = NormalizeToLatin(names);
            dict["scores"] = scores;

            Uri execUrl = BuildExecutionUrl("teachersByCriterion", dict);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Redirect);
            result.Headers.Location = execUrl;

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

        private static String NormalizeToLatin(String str)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in str)
            {
                if (CHAR_MAPPING.ContainsKey(c))
                {
                    result.Append(CHAR_MAPPING[c]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        private static readonly Dictionary<Char, String> CHAR_MAPPING = new Dictionary<char, String>()
        {
            {'А', "A"},
            {'Б', "B"},
            {'В', "V"},
            {'Г', "G"},
            {'Д', "D"},
            {'Е', "E"},
            {'Ж', "Zh"},
            {'З', "Z"},
            {'И', "I"},
            {'Й', "J"},
            {'К', "K"},
            {'Л', "L"},
            {'М', "M"},
            {'Н', "N"},
            {'О', "O"},
            {'П', "P"},
            {'Р', "R"},
            {'С', "S"},
            {'Т', "T"},
            {'У', "U"},
            {'Ф', "F"},
            {'Х', "H"},
            {'Ц', "Ts"},
            {'Ч', "Tc"},
            {'Ш', "Sh"},
            {'Щ', "Sht"},
            {'Ъ', "A"},
            {'Ю', "Yu"},
            {'Я', "Ya"},
            {'а', "a"},
            {'б', "b"},
            {'в', "v"},
            {'г', "g"},
            {'д', "d"},
            {'е', "e"},
            {'ж', "zh"},
            {'з', "z"},
            {'и', "i"},
            {'й', "j"},
            {'к', "k"},
            {'л', "l"},
            {'м', "m"},
            {'н', "n"},
            {'о', "o"},
            {'п', "p"},
            {'р', "r"},
            {'с', "s"},
            {'т', "t"},
            {'у', "u"},
            {'ф', "f"},
            {'х', "h"},
            {'ц', "ts"},
            {'ч', "tc"},
            {'ш', "sh"},
            {'щ', "sht"},
            {'ъ', "a"},
            {'ь', "u"},
            {'ю', "yu"},
            {'я', "ya"},
        };
    }
}
