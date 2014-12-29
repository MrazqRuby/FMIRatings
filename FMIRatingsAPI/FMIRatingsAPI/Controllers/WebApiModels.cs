using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Runtime.Serialization;
using FMIRatingsAPI.Models;

namespace FMIRatingsAPI.WebApiModels
{
	[DataContract]
	class Discipline
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public List<KeyValuePair<int, string>> Teachers { get; set; }
	}

	class Teacher
	{
		public string Name { get; set; }
		public List<Discipline> CoursesTaught { get; set; }
	}

	static class ModelFactory
	{
		private static DatabaseContext db = new DatabaseContext();

		public static List<Discipline> GetDisciplines()
		{
			var disciplines = db.Disciplines.Include(d => d.Tutors);
			List<Discipline> modelDisciplines = new List<Discipline>();
			foreach (var discipline in disciplines)
			{
				Discipline modelDiscipline = new Discipline();
				modelDiscipline.Name = discipline.Name;
				modelDiscipline.Description = discipline.Description;

				modelDiscipline.Teachers = new List<KeyValuePair<int, string>>();
				foreach (var teacher in discipline.Tutors)
				{
					modelDiscipline.Teachers.Add(new KeyValuePair<int, string>(teacher.Id, teacher.Name));
				}
				modelDisciplines.Add(modelDiscipline);
			}
			return modelDisciplines;
		}
	}
}