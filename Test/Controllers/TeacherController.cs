using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();//view of index
        }
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            return View(Teachers);//view of list
        }
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher FindTeacher = controller.FindTeacher(id);

            return View(FindTeacher);//view of show. find teacher based on id
        }
            
        
    }
}