using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Diagnostics;

namespace Test.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();//view of index
        }
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);//view of list
        }
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);//view of show. find teacher based on id
        }
        //GET : : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher FindTeacher = controller.FindTeacher(id);

            return View(FindTeacher);//view of Delete Confirm view
        }
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)//Redirect to the teachers list uppon clicking the delete button
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
      
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, DateTime hireDate, decimal salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            /// I tried passing to Create(, decimal salary, string employeenumber)
            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hireDate = hireDate;
            NewTeacher.salary = salary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
           
            //id the inputs that are provided in the form
            return RedirectToAction("List");
        }
        /// <summary>
        /// One is a get and the other a post request allowing us to update the teachers
        /// </summary>
        /// example /Teacher/Update/8
        /// <returns></returns>
        //GET : /Teacher/Update/{id}
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hireDate, decimal salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;
            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.hireDate = hireDate;
            TeacherInfo.salary = salary;


            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }

    }
}