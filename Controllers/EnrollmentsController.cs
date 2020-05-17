using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult EnrollStudent(Student newStudent)
        {

            using (var con = new SqlConnection("data source=db-mssql;initial catalog=s16479;integrated security=true"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName,LastName,BirthDate,Name,Semester from Student as st inner join Enrollment as e on st.IdEnrollment = e.IdEnrollment inner join Studies as s on e.IdStudy = s.IdStudy";

                con.Open();
                var dr = com.ExecuteReader();



            }
        }
    }
 }