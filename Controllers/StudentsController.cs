using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]

        public IActionResult GetStudent(string orderBy)
        {
/*            return Ok(_dbService.GetStudents());
*/
            using (var con = new SqlConnection("data source=db-mssql;initial catalog=s16479;integrated security=true"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName,LastName,BirthDate,Name,Semester from Student as st inner join Enrollment as e on st.IdEnrollment = e.IdEnrollment inner join Studies as s on e.IdStudy = s.IdStudy";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                }
                
            }

        }

      

[HttpGet("{id}")]

        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            } else if (id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]

        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateStudent(Student student)
        {
            return Ok("Akutalizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStduent()
        {
            return Ok("Usuwanie zakończone");
        }

    }
}