using FMIRatingsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FMIRatingsAPI.Authentication
{
    public class UserPrincipal : GenericPrincipal
    {
        public User user { get; set; }

        public UserPrincipal(string name, string[] roles, User user)
            : base(new GenericIdentity(name), roles)
        {
            this.user = user;  
        }

    }
}