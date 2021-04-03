using Microsoft.AspNetCore.Mvc;
using Student.Models;
using System.Collections.Generic;
using System.Linq;


namespace Student.Controllers
{
    public class StudentController : Controller
    {
        private Student_tblContext _ORM=null;
        public StudentController(Student_tblContext ORM)
        {
           this._ORM = ORM;
           
        }
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(Studenttbl S)
        {
            _ORM.Studenttbl.Add(S);
            _ORM.SaveChanges();
            ViewBag.Message = "Registration Done Succefully!";
            return View(S);
        }

        public IActionResult AllStudents()
        {
            IList<Studenttbl> Studentlist = _ORM.Studenttbl.ToList<Studenttbl>();
            return View(Studentlist);
        }

        [HttpPost]  
        public IActionResult AllStudents(int StudentId, string Firstname, string Lastname, string Dept, int Rollno)
        {
            IList<Studenttbl> Studentlist = _ORM.Studenttbl.Where(m => m.StudentId.Equals(StudentId) || m.Firstname.Contains(Firstname) || m.Lastname.Contains(Lastname) || m.Rollno.Equals(Rollno)).ToList<Studenttbl>();
            return View(Studentlist);
        }

        public IActionResult StudentDetail(int StudentId)
        {
            return View(_ORM.Studenttbl.Where(m=>m.StudentId.Equals(StudentId)).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult EditStudent(int Id)
        {
            Studenttbl S = _ORM.Studenttbl.Where(m => m.StudentId.Equals(Id)).FirstOrDefault();
            return View(S);
        }


        public string GetStudentsNames()
        {
            string Result = "";

            var r = Request;

            IList<Studenttbl> All = _ORM.Studenttbl.ToList<Studenttbl>();
            Result += "<h1 class='alert alert-success'>Total Students: " + All.Count + "</h1>";

            foreach (Studenttbl S in All)
            {
                Result += "<a href='/Studenttbl/StudentDetail?StudentId=" + S.StudentId + "'><p><span class='glyphicon glyphicon-user'></span> " + S.Firstname + "</p></a> <a href='/student/deletestudent1?id=" + S.StudentId + "'>Delete</a>";
            }

            return Result;
        }
            [HttpPost]
        public IActionResult EditStudent(Studenttbl S)
        {
            try
            {
                _ORM.Studenttbl.Update(S);
                _ORM.SaveChanges();
                ViewBag.MessageSucess = "Record Updated Succefully";
            }
            catch
            {
                ViewBag.MessageFailure = "Error! Could not Update Record";
            }
            return View(S);
        }

        public IActionResult DeleteStudent(Studenttbl S)
        {
            _ORM.Studenttbl.Remove(S);
            _ORM.SaveChanges();
            return RedirectToAction(nameof(StudentController.AllStudents));
        }

        public string DeleteStudentByAjax(Studenttbl s)
        {
            string result;
            try
            {
                _ORM.Studenttbl.Remove(s);
                _ORM.SaveChanges();
                result = "Yes";
            }
            catch
            {
                result = "No";
            }
            return result;
        }


    }


}
