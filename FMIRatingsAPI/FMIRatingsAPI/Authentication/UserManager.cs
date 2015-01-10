using FMIRatingsAPI.DAL;
using FMIRatingsAPI.Models;
using FMIRatingsAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FMIRatingsAPI.Authentication
{
    public static class UserManager
    {
        static UserManager()
        {
            DbContext = new FMIRatingsContext();    
        }

        public static User GetUser(string username)
        {
            return DbContext.Users.SingleOrDefault(u => u.Name == username);
        }

        public static User GetCurrentUser()
        {
            return (HttpContext.Current.User as UserPrincipal).user;
        }

        public static User CreateUser(UserWithPasswordDTO user)
        {
            User newUser = new User
            {
                Name = user.Name,
                Password = user.Password,
                RealName = user.RealName,
                Email = user.Email,
                Admin = false,
                Course = user.Course,
                Group = user.Group,
                Major = user.Major
            };
            if (GetUser(user.Name) != null)
            {
                return null;
            }
            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();
            return newUser;
        }


        private static FMIRatingsContext DbContext { get; set; }
    }
}