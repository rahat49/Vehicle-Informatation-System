using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VIMS.Data;
using VIMS.DataModel;

namespace VIMS.Controllers
{
    public class TrafficPoliceController : Controller
    {

        private readonly BRTAContext  _context;

        public TrafficPoliceController(BRTAContext context)
        {
            _context = context;
        }

        //-----------------------------TrafficPolice Login,Logout
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(TrafficPolice model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var check = _context.Trafficpolice.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (check != null)
            {
                HttpContext.Session.SetInt32("UserId", check.Tid);
                HttpContext.Session.SetString("Name", check.Name);
                return RedirectToAction("TpDashboard", "TrafficPolice");

            }
            TempData["error"] = "Login Information Wrong!";
            return RedirectToAction("Login");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult TpDashboard()
        {
            //rulesviolation
            var rv = _context.Rulesviolations.ToList();
            ViewBag.rulesviolation = rv.Count();

            //accident
            var ac = _context.AccidentCases.ToList();
            ViewBag.accident = ac.Count();
            {
                ViewBag.abc = HttpContext.Session.GetInt32("UserId");


                if (ViewBag.abc != null && ViewBag.abc != 0)
                {
                   
                    return View();
                }
                else
                {
                    TempData["msg"] = "Invalid!";
                    return RedirectToAction("Login");
                   
                }
            }     
        }


 //----------------------------User Profile-------------------------
        public IActionResult TpProfile()
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");


            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(_context.Trafficpolice.Where(e => e.Tid == userID));
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }
            
            
        }
       
        [HttpGet]
        public IActionResult TpProfileEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var update = _context.Trafficpolice.Find(id);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TpProfileEdit(TrafficPolice model)
        {
            if (ModelState.IsValid)
            {
                _context.Trafficpolice.Update(model);
                _context.SaveChanges();
                TempData["success"] = "Data Updated Done!";
                return RedirectToAction("TpProfile");

            }
            return RedirectToAction("TpProfile");

        }

     //------------------------Rules Violation---------------------
        public IActionResult RulesViolation()
        {
                ViewBag.abc = HttpContext.Session.GetInt32("UserId");

                if (ViewBag.abc != null && ViewBag.abc != 0)
                {

                    return View();
                }
                else
                {
                    TempData["msg"] = "Invalid!";
                    return RedirectToAction("Login");

                }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RulesViolation(RulesViolationForm Violation)
        {
            if (_context.Vehicles.SingleOrDefault(x => x.Vehnum == Violation.Vehnum) == null)
            {
                return NotFound();

            }
            else 
            {
                _context.Rulesviolations.Add(Violation);
                _context.SaveChanges();
                TempData["success"] = "Rules Violation Case Successfully Created!";
                return RedirectToAction("rulesViolationlist");
            }
           
        }
        public IActionResult rulesViolationlist()

        {
            ViewBag.sas = HttpContext.Session.GetString("Name");
            string ab = ViewBag.sas.ToString();
            return View(_context.Rulesviolations.Where(e => e.TPoliceName == ab).ToList());
           
        }
       


        //----------------------Accident  form--------------
        public IActionResult AddAccidentCase()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View();
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }

        }

        [HttpPost]
        public IActionResult AddAccidentCase(AccidentCase  accident)
        {
            if (_context.Vehicles.SingleOrDefault(x=>x.Vehnum==accident.Vehnum)==null)
            {
                return NotFound();

            }

            else
            {
                _context.AccidentCases.Add(accident);
                _context.SaveChanges();
                TempData["success"] = "Accident Info Successfully Created!";
                return RedirectToAction("Accidentlist");
            }
            
        }

        //---------------------Report----------------------------
        //Rulesviolation
        public async Task<IActionResult> RulesViolationReport()
        {
            var rv = _context.Rulesviolations.ToList();
            ViewBag.rulesviolation = rv.Count();

            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Rulesviolations.ToListAsync());
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            } 
           

          
        }



        //Accidnet case  data
        public async Task<IActionResult> Accidentlist()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.AccidentCases.ToListAsync());
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }
            
        }
        public async Task<IActionResult> AccidentReport()
        {

            var ac = _context.AccidentCases.ToList();
            ViewBag.accident = ac.Count();

            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.AccidentCases.ToListAsync());
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }
            
        }

        //-------------------------PDF---------------------------
        public async Task<IActionResult> PDFRulesViolation()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Rulesviolations.ToListAsync());
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }
           
        }
        public async Task<IActionResult> PDFAccident()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.AccidentCases.ToListAsync());
            }
            else
            {
                TempData["msg"] = "Invalid!";
                return RedirectToAction("Login");

            }
           
        }
    }
}