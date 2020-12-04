///My code is from Christine Bittle's video and blog DB. Used as a reference
///


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Test.Models;
using System.Diagnostics;

namespace Test.Controllers
{

    public class TeacherDataController : ApiController
    {
        private SchoolDbContext teacherdatabase = new SchoolDbContext();
        //api/TeacherData/ListTeachers
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            //Cange sql query to utilize the new string SearchKey

            cmd.CommandText = "select * from teachers WHERE lower(teacherfname) " +
                "like lower(@key) or + lower(teacherlname) " +
                "like lower(@key) or " + "lower(concat(teacherfname, ' ', teacherlname)) " +
                "like lower(@key)";
            //prevent an attack due to symols ending a query and possibly using a delet or updat function.
            cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher> { };///list of teachers from db
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string AuthorFname = (string)ResultSet["teacherfname"];
                string AuthorLname = (string)ResultSet["teacherlname"];
                //string EmployeeNumber = (string)ResultSet["employeenumber"];
                //DateTime HireDate = (DateTime)ResultSet["hiredate"];//date time as seen in footer
                //decimal Salary = ResultSet.GetDecimal(ResultSet.GetOrdinal("salary"));


                Teacher FindTeacher = new Teacher();
                FindTeacher.teacherid = TeacherId;
                FindTeacher.teacherfname = AuthorFname;
                FindTeacher.teacherlname = AuthorLname;
                //FindTeacher.employeenumber = EmployeeNumber;
                //FindTeacher.hireDate = HireDate;
                //FindTeacher.salary = Salary;

                Teachers.Add(FindTeacher);
            }
            conn.Close();
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)//use teacher id to fine all of their relevant info
        {
            Teacher FindTeacher = new Teacher();
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from teachers where teacherid =" + id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                //string EmployeeNumber = (string)ResultSet["employeenumber"];
                //DateTime HireDate = (DateTime)ResultSet["hiredate"];//date time as seen in footer
                //decimal Salary = ResultSet.GetDecimal( ResultSet.GetOrdinal( "salary" ) );

                FindTeacher.teacherid = TeacherId;
                FindTeacher.teacherfname = TeacherFname;
                FindTeacher.teacherlname = TeacherLname;
                //FindTeacher.employeenumber = EmployeeNumber;
                //FindTeacher.hireDate = HireDate;
                //FindTeacher.salary = Salary;

            }

            return FindTeacher;

        }
        /// <summary>
        /// POst Request for deletion
        /// <example> POST : /api/TeacherData/DeleteTeacher/2</example>
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        //[Route("")]<--- doesent ned to be specified as we are following the action convention
        public void DeleteTeacher(int id)//Deletes teacher permanently
        {
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            //string query = "Delete from authors where teacherid=@id";
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();

        }
/// <summary>
/// /I was able to add the first and last name of the teachers to the db however when I tried to do it to 
/// the employee number and date it completely stopped working (List View) I have no idea how or why. I have commented out 
/// all the Hire date data, Salary Data and the employee number. Delete functionality works, Search bar works.
/// At this point i am commiting to git hub and will work to fix the issue if i can.  If you are able to tell me what :
///pServer Error in '/' Application: Specified cast not valid. line 42 datacontroller
/// </summary>
/// <param name="NewTeacher"></param>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname) values(@teacherfname, @teacherlname)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            ///cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);<-- I know I add them like this and add them to the above sql statement but it kept throwing errors on line 41-43
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }

}
