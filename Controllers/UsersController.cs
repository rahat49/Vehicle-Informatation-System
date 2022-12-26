using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Debugger.Contracts;
using VIMS.Data;
using VIMS.DataModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VIMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly BRTAContext _context;

        public UsersController(BRTAContext context)
        {
            _context = context;
        }


        //----------------------------Signup for user
        public IActionResult SignUp()
        {
            return View();
        }
        //user registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> SignUp(User user)
        {
          
            if ((await _context.Vehicles.AnyAsync(x => x.Vehnum == user.Vehnum)) && (!await _context.User.AnyAsync(x => x.Vehnum ==user.Vehnum)))
            {
               _context.User.Add(user);
                _context.SaveChanges();
                TempData["success"] = "Registation Successful!";
                return RedirectToAction("Login");
            }
            TempData["error"] = "Registration Error.";

            return RedirectToAction("SignUp");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var check = _context.User.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (check != null)
            {
                HttpContext.Session.SetInt32("UserId", check.Uid);
                HttpContext.Session.SetString("Vehnum", check.Vehnum);
                return RedirectToAction("Home", "Users");
            }
            TempData["error"] = "Login Information Wrong!";
            return RedirectToAction("Login");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Home()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");


            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                return View();
            }
            TempData["error"] = " Please Login First!";
            return RedirectToAction("Login");

        }
        //-----------------------------User Profile------------------------->
        public IActionResult UserProfile()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                int userID = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                return View(_context.User.Where(e => e.Uid == userID));

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
           
        }

        
        public async Task<IActionResult> CitizenEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tp = await _context.User.FindAsync(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CitizenEdit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var not = _context.User.First(x => x.Uid == id);
                    not.Name = user.Name;
                    not.Email = user.Email;
                    not.Phone = user.Phone;
                    not.NID = user.NID;
                    not.Vehnum = user.Vehnum;
                    not.Address = user.Address;
                    not.Password = user.Password;

                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return View(user);

                }
                TempData["success"] = "Data Updated Done!";
                return RedirectToAction("UserProfile");
            }
            return View(user);
        }

      

        //-------------------------------User Report------------------------------------
        //Accident report
        public IActionResult AccidentReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                ViewBag.sas = HttpContext.Session.GetString("Vehnum");
                string ab = ViewBag.sas.ToString();

                return View(_context.AccidentCases.Where(e => e.Vehnum == ab).ToList());

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

           
        }
        //Violation Report
        public IActionResult ViolationReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                ViewBag.sas = HttpContext.Session.GetString("Vehnum");
                string ab = ViewBag.sas.ToString();
                return View(_context.Rulesviolations.Where(e => e.Vehnum == ab).ToList());

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
           
        }
        //--------------_Service
        public async Task<IActionResult> Service()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                return View(await _context.Services.ToListAsync());

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
            
        }

        //------------------------Renew Apply--------------------------

        public IActionResult RenewApply()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                return View();

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

          
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RenewApply(Renewapply renewapply)
        {

         if(_context.Vehicles.SingleOrDefault(x => x.Vehnum == renewapply.Vehnum) == null)
            {
                TempData["error"] = "Application Error!";
                return RedirectToAction("Renew Apply");
            }
             else
            {
                _context.Renewapplies.Add(renewapply);
                _context.SaveChanges();

                
                var allapp = _context.Renewapplies.OrderByDescending(x => x.id).ToList();
                TempData["success"] = "Application Done!";
                return RedirectToAction("RenewStatus");
                
            }
        
              
        }
       
        public IActionResult RenewStatus()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                ViewBag.sas = HttpContext.Session.GetString("Vehnum");
                string ab = ViewBag.sas.ToString();
                return View(_context.Renewapplies.Where(e => e.Vehnum == ab).ToList());

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
        }

       
        public async Task<IActionResult> FitnessApplicationView(int? id)
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                if (id == null || _context.Renewapplies == null)
                {
                    return NotFound();
                }

                var user = await _context.Renewapplies
                    .FirstOrDefaultAsync(m => m.id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }


           
        }

    }
}



