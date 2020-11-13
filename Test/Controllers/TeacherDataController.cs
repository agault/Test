///My code is from Christine Bittle's video and blog DB. Used as a reference
///


using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Test.Models;

namespace Test.Controllers
{

    public class TeacherDataController : ApiController
    {
        private SchoolDbContext teacherdatabase = new SchoolDbContext();
        //api/TeacherData/ListTeachers
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            MySqlConnection conn = teacherdatabase.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> Teachers = new List<Teacher> { };///list of teachers from db
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string AuthorFname = (string)ResultSet["teacherfname"];
                string AuthorLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];//try Date time, its in the footer folder and I think its what is needed
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher FindTeacher = new Teacher();
                FindTeacher.teacherid = TeacherId;
                FindTeacher.teacherfname = AuthorFname;
                FindTeacher.teacherlname = AuthorLname;
                FindTeacher.employeenumber = EmployeeNumber;
                FindTeacher.hireDate = HireDate;
                FindTeacher.salary = Salary;

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
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];//date time as seen in footer
                decimal Salary = (decimal)ResultSet["salary"];

                FindTeacher.teacherid = TeacherId;
                FindTeacher.teacherfname = TeacherFname;
                FindTeacher.teacherlname = TeacherLname;
                FindTeacher.employeenumber = EmployeeNumber;
                FindTeacher.hireDate = HireDate;
                FindTeacher.salary = Salary;

            }

            return FindTeacher;

        }
    }

}
