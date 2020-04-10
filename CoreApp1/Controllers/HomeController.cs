using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Controllers
{
   // [Route("Home")]
    public class HomeController: Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IHostingEnvironment hostingEnvironment;
      

        public HomeController(IEmployeeRepository employeeRepository)// ,IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            
        }
        public ViewResult Index()
        {
            //return _employeeRepository.GetEmployee(4).Name;
            var model = _employeeRepository.GetAllEmployee();
            return View(model);

        }

        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(4);
        //    return Json(model);
        //}

        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(2);
        //    return new ObjectResult(model);
        //}


        //[Route("")]
        //  [Route("Home")]
         //[Route("Home/Index1")]
        //  [Route("~/")]
       // [Route("Index1")]
        public ViewResult Index1()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

       // [Route("~/")]
       // [Route("Details/{id?}")]
        public ViewResult Details(int? id )
        {
            //throw new Exception("Error in Details View");

            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homedetails = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };

            return View(homedetails);
            
            //Employee model = _employeeRepository.GetEmployee(3);

            //using ViewData
            // ViewData["Employee"] = model;
            // ViewData["PageTitle"] = "Employee Details";


            //Using ViewBag

           // ViewBag.Employee = model;
           // ViewBag.PageTitle = "Employee Details";

           //  return View(model);
           
            
            // return View("Test");  //from views/home folder 


            //from new folder
           // return View("MyViews/Test.cshtml");

           // return View("../Test/Update");
        }

        [HttpGet]
        //[Route("Home/Create")]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        //Without required field validator

        //[HttpPost]
        //public RedirectToActionResult Create(Employee employee)
        //{
        //   Employee newEmployee = _employeeRepository.Add(employee);
        //    return RedirectToAction("Details", new { id = newEmployee.Id });     
        //}

        //With Validators

        [HttpPost]
        [Authorize]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("Details", new { id = newEmployee.Id });

            }
            return View();
        }

        

        //USING FILE UPLOAD THIS METHOD IS USED

        //[HttpPost]
        //public IActionResult Create(EmployeeCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = null;
        //        if (model.Photo != null)
        //        {
        //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }

        //        Employee newEmployee = new Employee
        //        {
        //            Name = model.Name,
        //            Email = model.Email,
        //            Department = model.Department,
        //            PhotoPath = uniqueFileName
        //        };

        //        _employeeRepository.Add(newEmployee);
        //        return RedirectToAction("Details", new { id = newEmployee.Id });

        //    }
        //    return View();
        //}

        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department
            };
            return View(employeeEditViewModel);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                
              
                _employeeRepository.Update(employee);
                return RedirectToAction("Index1");

            }
            return View();
        }
    }
}
