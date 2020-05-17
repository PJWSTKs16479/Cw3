using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace Cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            var st = new Student();
            st.FirstName = request.FirstName;
            st.LastName = request.LastName;

            using (var con = new SqlConnection("data source=db-mssql;initial catalog=s16479;integrated security=true;MultipleActiveResultSets=true"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //1.Czy studia istnieja?

                    com.CommandText = "select IdStudy from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    com.Transaction = tran;
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Takich studiow nie ma");
                    }
                    int idStudies = (int)dr["IdStudy"];
                    dr.Close();

                    int IdEnrollment = 4;
                    com.CommandText = "select IdEnrollment from Enrollment where Semester = @Semester and IdStudy = @IdStudy";
                    com.Parameters.AddWithValue("IdStudy", idStudies);
                    com.Parameters.AddWithValue("Semester", 1);
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Read();
                        com.CommandText = "Insert into Enrollment(IdEnrollment, Semester, IdStudy, StartDate) values (@IdEnrollment, @Semester1, @IdStudy1, @StartDate)";
                        com.Parameters.AddWithValue("IdEnrollment", IdEnrollment);
                        com.Parameters.AddWithValue("Semester1", 1);
                        com.Parameters.AddWithValue("IdStudy1", idStudies);
                        com.Parameters.AddWithValue("StartDate", DateTime.Now);
                        dr.Close();
                        com.ExecuteNonQuery();

                    }
                    else
                    {
                        IdEnrollment = (int)dr["IdEnrollment"];
                        dr.Close();
                    }

                   
                    com.CommandText = "select * from Student where IndexNumber = @IndexNumber";
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        return BadRequest("Taki numer indeksu już istnieje w bazie");
                    }
                    dr.Close();
                   


                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) Values (@Index, @FirstName, @LastName, @BirthDate, @IdEnrollment1)";
                    com.Parameters.AddWithValue("Index", request.IndexNumber);
                    com.Parameters.AddWithValue("FirstName", request.FirstName);
                    com.Parameters.AddWithValue("LastName", request.LastName);
                    com.Parameters.AddWithValue("BirthDate", DateTime.Parse(request.BirthDate));
                    com.Parameters.AddWithValue("IdEnrollment1", IdEnrollment);
                    com.ExecuteNonQuery();


                    tran.Commit();
                    return Ok("Zapisano studenta");


                }
                catch (SqlException exception)
                {
                    tran.Rollback();
                    return BadRequest("Blad: " + exception);
                }

            }

            //var response = new EnrollStudentResponse();
            //response.LastName = st.LastName;


            //    return Ok(response);

        }
     }
}
 