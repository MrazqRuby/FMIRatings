using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;
using FMIRatingsAPI.Statistics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
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

        private Dictionary<String, Object> GetData(String transform, String input)
        {
            int inputId = System.Int32.Parse(input);
            InputTransformer transformer = new InputTransformer(transform, _db);
            return transformer.Transform(inputId);
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
    }
}
