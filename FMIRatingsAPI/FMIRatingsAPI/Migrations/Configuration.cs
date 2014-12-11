namespace FMIRatingsAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FMIRatingsAPI.Models;
    using System.Collections;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<FMIRatingsAPI.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false; // Don't touch this
        }

        protected override void Seed(FMIRatingsAPI.Models.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.
            User rootAccout = new User();
            rootAccout.Id = 1;
            rootAccout.Name = "admin";
            rootAccout.Password = "admin";
         
            context.Users.AddOrUpdate(user => user.Name, rootAccout);

            Discipline PISS = new Discipline();
            PISS.Id = 2;
            PISS.Name = "ПИСС";
            PISS.Description = "yellow, liquid";

            Teacher PISSTeacher = new Teacher();
            PISSTeacher.Name = "Unknown";
            PISSTeacher.CoursesTaught.Add(PISS); // Equivalent to PISS.Tutors.Add(PISSTeacher);
            
            Discipline Analiz = new Discipline();
            Analiz.Id = 1;
            Analiz.Name = "Анализ 2.5";
            Analiz.Description = "Без анализ животът е празен";

            Teacher Babev = new Teacher();
            Babev.Name = "Babev";
            Babev.CoursesTaught.Add(Analiz);

            Teacher Ribarska = new Teacher();
            Ribarska.Name = "Ribarska";
            Ribarska.CoursesTaught.Add(Analiz);
            

            context.Teachers.AddOrUpdate(teacher => teacher.Name, PISSTeacher);
            context.Teachers.AddOrUpdate(teacher => teacher.Name, Babev);
            context.Teachers.AddOrUpdate(teacher => teacher.Name, Ribarska);
            context.Disciplines.AddOrUpdate(d => d.Name, PISS);
            context.Disciplines.AddOrUpdate(d => d.Name, Analiz);

            context.SaveChanges();
        }
    }
}
