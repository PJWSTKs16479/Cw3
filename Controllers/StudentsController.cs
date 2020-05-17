using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        [HttpGet]

        public IActionResult GetStudents()
        {
            var _students = new List<Student>();

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
                    _students.Add(st);
                }

            }
            return Ok(_students);
        }

        [HttpGet("{indexNumber}")]

        public IActionResult GetStudent(string indexNumber)
        {
            using (var con = new SqlConnection("data source=db-mssql;initial catalog=s16479;integrated security=true"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                /*com.CommandText = "select * from student,enrollment where indexnumber = '"+indexNumber+"' AND student.idenrollment= enrollment.idenrollment";*/

                com.CommandText = "select * from student,enrollment where indexnumber = @index AND student.idenrollment= enrollment.idenrollment";

                /*SqlParameter par = new SqlParameter();
                par.Value = indexNumber;
                par.ParameterName = "index";
                com.Parameters.Add(par);*/

                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();

                if (dr.Read())
                {
                    var st = new Student();
                    st.IdEnrollment = dr["IdEnrollment"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.IdStudy = dr["IdStudy"].ToString();
                    st.StartDate = dr["StartDate"].ToString();
                    return Ok(st);
                }
            }
            return NotFound();
        }






        /*private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]

        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudents());

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
        }*/

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