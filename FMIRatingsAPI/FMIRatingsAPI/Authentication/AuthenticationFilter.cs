using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FMIRatingsAPI.Authentication
{
    class UnathorizedErrorResult : IHttpActionResult
    {
        private HttpResponseMessage _response;

        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(_response);
        }

        public UnathorizedErrorResult(HttpRequestMessage request, string errorMessage)
        {
            _response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _response.RequestMessage = request;
            _response.Content = new StringContent(errorMessage);
        }
    }

    public class AuthenticationFilter : System.Attribute, IAuthenticationFilter
    {

        public System.Threading.Tasks.Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeader = context.Request.Headers.Authorization;
            if (authHeader == null)
            {
                context.ErrorResult = new UnathorizedErrorResult(context.Request, "Authentication failure");
            }
            else
            {
                switch (authHeader.Scheme)
                {
                    case "Basic":
                        {
                            var dbcontext = new FMIRatingsAPI.DAL.FMIRatingsContext();
                            string user;
                            string pass;
                            try
                            {
                                byte[] data = Convert.FromBase64String(authHeader.Parameter);
                                string[] credentials = Encoding.UTF8.GetString(data).Split(':');
                                user = credentials[0];
                                pass = credentials[1];
                            }
                            catch (System.Exception exc)
                            {
                                context.ErrorResult = new UnathorizedErrorResult(context.Request, exc.Message);
                                break;
                            }
                            if (dbcontext.Users.Any(u => u.Name == user && u.Password == pass))
                            {
                                // Set principal
                            }
                            else
                            {
                                context.ErrorResult = new UnathorizedErrorResult(context.Request, "Authentication failure");
                            }
                            break;
                        }
                }
            }
            return System.Threading.Tasks.Task.FromResult(0);
        }

        public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}