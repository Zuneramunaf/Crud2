using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _DBContext;

        public HomeController(ApplicationDbContext DBContext)
        {
            _DBContext= DBContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid){

                _DBContext.Students.Add(student);
                _DBContext.SaveChanges();
                TempData["Success"] = "Successfully Registered";
                return RedirectToAction("ViewStudent", "Home");
            }
            return View();
        }
        [HttpGet]                                                                                                                         
        public IActionResult ViewStudent()
        {
            var student =_DBContext.Students.ToList();
            return View(student);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _DBContext.Students.Find(id);   
            //.FirstorDefault(x => x.Id == id)
            return View(student);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _DBContext.Students.Find(id);
            //.FirstorDefault(x => x.Id == id)
            return View(student);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {

                _DBContext.Students.Update(student);
                _DBContext.SaveChanges();
                TempData["Update"] = "Student Update Successfully";
                return RedirectToAction("ViewStudent","Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _DBContext.Students.Find(id);

            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
           
            

              _DBContext.Students.Remove(student);
                _DBContext.SaveChanges();
                TempData["Delete"] = "Student Delete Successfully";
                return RedirectToAction("ViewStudent", "Home");
            

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
