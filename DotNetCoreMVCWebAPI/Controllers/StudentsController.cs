using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howest;

namespace DotNetCoreMVCWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        List<Student> studenten;

        public StudentsController()
        {
            studenten= new List<Student>
            {
                new Student {Id=23, Naam="Jefke", AfstudeerGraad=Graad.Voldoening },
                new Student {Id=45, Naam="Marieke", AfstudeerGraad=Graad.Onderscheiding },
            };
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(studenten);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            Student student = studenten.FirstOrDefault(s => s.Id == id);
            if (student == null) {
                return NotFound($"Student met id {id} is niet gevonden");
            }
            return Ok(student);
        }
    }
}