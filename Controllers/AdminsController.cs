using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIMS.Data;
using VIMS.DataModel;

namespace VIMS.Controllers
{
    public class AdminsController : Controller
    {
        private readonly BRTAContext _context;

        public AdminsController(BRTAContext context)
        {
            _context = context;
        }
        //-----------------------------BRTA Login,Logout
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var check = _context.BRTA.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (check != null)
            {
                HttpContext.Session.SetInt32("UserId", check.id);
                
                return RedirectToAction("AdminDashboard", "Admins");


            }
            TempData["error"] = "Login Information Wrong!";
            return RedirectToAction("Login");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        //-----------------------------Admin Dashboard home page
        public IActionResult AdminDashboard()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                //trafficpolice
                var ra = _context.Trafficpolice.ToList();
                ViewBag.Trafficpolice = ra.Count();
                //user
                var us = _context.User.ToList();
                ViewBag.user = us.Count();

                //Total user

                ViewBag.total = ra.Count() + us.Count();

                //rulesviolation
                var rv = _context.Rulesviolations.ToList();
                ViewBag.rulesviolation = rv.Count();

                //accident
                var ac = _context.AccidentCases.ToList();
                ViewBag.accident = ac.Count();



                //fitness renew application
                var rp = _context.Renewapplies.Where(x => x.Status == "Pending").ToList();
                ViewBag.pending = rp.Count();
                //fitness renew approved 
                rp = _context.Renewapplies.Where(x => x.Status == "Approved").ToList();
                ViewBag.approve = rp.Count();
                //finess renew reject
                rp = _context.Renewapplies.Where(x => x.Status == "Rejected").ToList();
                ViewBag.reject = rp.Count();
                //vehicle
                var veh = _context.Vehicles.ToList();
                ViewBag.vehicle = veh.Count();

                //notice
                var n = _context.Notices.ToList();
                ViewBag.notice = n.Count();
                //accident rate

                return View();

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
           
        }
        //------------------------------citizen data
        public async Task<IActionResult> CitizenData()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.User.ToListAsync());
            }
            else
            {

                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
          
        }
        //citizen data detail
        public async Task<IActionResult> CitizenDetails(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Uid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //citizen data delete
        [HttpGet]
        public IActionResult CitizenDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost, ActionName("CitizenDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();

            }
            _context.User.Remove(user);
            _context.SaveChanges();
            TempData["success"] = "Citizen Data Deleted Succcessful!";
            return RedirectToAction("CitizenData");
        }

        //------------------------Traffic Police---------------------
        public IActionResult TpoliceCreate()
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
        public IActionResult TpoliceCreate(TrafficPolice police)
        {
            if (ModelState.IsValid)
            {
                _context.Trafficpolice.Add(police);
                _context.SaveChanges();
                TempData["success"] = "Traffic Police Account Created!";
                return RedirectToAction("TpoliceList");
            }
            return View(police);
        }

        public async Task<IActionResult> TpoliceList()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Trafficpolice.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

           
        }

        public async Task<IActionResult> TpoliceEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tp = await _context.Trafficpolice.FindAsync(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TpoliceEdit(TrafficPolice trafficPolice)
        {
            if (ModelState.IsValid)
            {
                _context.Trafficpolice.Update(trafficPolice);
                _context.SaveChanges();
                TempData["success"] = "Traffic Police Data Updated Done!";
                return RedirectToAction("TpoliceList");
            }
            return View(trafficPolice);
        }

        public async Task<IActionResult> TpoliceDetails(int? id)
        {
            if (id == null || _context.Trafficpolice == null)
            {
                return NotFound();
            }

            var tp = await _context.Trafficpolice
                .FirstOrDefaultAsync(m => m.Tid == id);
            if (tp == null)
            {
                return NotFound();
            }

            return View(tp);
        }


        [HttpGet]
        public IActionResult TpoliceDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tp = _context.Trafficpolice.Find(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);

        }
        [HttpPost, ActionName("TpoliceDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult TpDeleteData(int? id)
        {
            var tp = _context.Trafficpolice.Find(id);
            if (tp == null)
            {
                return NotFound();

            }
            _context.Trafficpolice.Remove(tp);
            _context.SaveChanges();
            TempData["success"] = "Traffic Police Account Deleted Succcessful!";
            return RedirectToAction("Tpolicelist");
        }

        //--------------------Fitness renewrequest------------------------------
        public async Task<IActionResult> RenewRequest()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Renewapplies.Where(x => x.Status == "Pending").ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
        //Fitness pending detail

        public async Task<IActionResult> RenewAppdetail(int? id)
        {
            if (id == null || _context.Renewapplies == null)
            {
                return NotFound();
            }

            var app = await _context.Renewapplies
                .FirstOrDefaultAsync(m => m.id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }


        //Fitness approved detail

        public async Task<IActionResult> RenewApprovedDetail(int? id)
        {
            if (id == null || _context.Renewapplies == null)
            {
                return NotFound();
            }

            var app = await _context.Renewapplies
                .FirstOrDefaultAsync(m => m.id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        //fitness accept
        public async Task<IActionResult> Accept(int? id)


        {
            try
            {
                var ab = await _context.Renewapplies
               .FirstOrDefaultAsync(m => m.id == id);
                ab.Status = "Approved";
                _context.SaveChanges();
                TempData["success"] = "Request Accepted!";
                return RedirectToAction("RenewApproved");

            }
            catch
            {
                return View();

            }


        }
        //Fitness Renew Approved
        public async Task<IActionResult> RenewApproved()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Renewapplies.Where(x => x.Status == "Approved" || x.Status == "Rejected").ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }


            

        }

        //Fitness request Rejected
        public async Task<IActionResult> Reject(int? id)


        {
            try
            {
                var re = await _context.Renewapplies
               .FirstOrDefaultAsync(m => m.id == id);
                re.Status = "Rejected";
                _context.SaveChanges();
                return RedirectToAction("RenewApproved");

            }
            catch
            {
                return View();

            }


        }
        //----------------Start Notice--------------------------
        public IActionResult Notice()
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
        public IActionResult Notice(Notice notice)
        {
            if (ModelState.IsValid)
            {
                _context.Notices.Add(notice);
                _context.SaveChanges();
                TempData["success"] = "Notice Created Done!";
                return RedirectToAction("NoticeView");
            }
            return View(notice);
        }

        public async Task<IActionResult> NoticeView()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Notices.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }

        [HttpGet]
        public IActionResult NoticeEdit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }


            var createProfile = _context.Notices.Find(Id);
            if (createProfile == null)
            {
                return NotFound();
            }
            return View(createProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NoticeEdit(Notice model)
        {
            if (ModelState.IsValid)
            {
                _context.Notices.Update(model);
                _context.SaveChanges();
                TempData["success"] = "Notice Updated Done!";
                return RedirectToAction("NoticeView");

            }
            return RedirectToAction("NoticeView");

        }
        public async Task<IActionResult> NoticeDetails(int? id)
        {
            if (id == null || _context.Notices == null)
            {
                return NotFound();
            }

            var nd = await _context.Notices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nd == null)
            {
                return NotFound();
            }

            return View(nd);
        }
        [HttpGet]
        public IActionResult NoticeDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tp = _context.Notices.Find(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);

        }
        [HttpPost, ActionName("NoticeDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult NoitceDeleteData(int? id)
        {
            var Nd = _context.Notices.Find(id);
            if (Nd == null)
            {
                return NotFound();

            }
            _context.Notices.Remove(Nd);
            _context.SaveChanges();
            TempData["success"] = "Notice Deleted Done!";
            return RedirectToAction("NoticeView");
        }
        //----------------------End Notice-----------------------
        //----------------Start Service--------------------------
        public IActionResult ServiceCreate()
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
        public IActionResult ServiceCreate(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                TempData["success"] = "Service Created Done!";
                return RedirectToAction("ServiceView");
            }
            return View(service);
        }

        public async Task<IActionResult> ServiceView()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {

                return View(await _context.Services.ToListAsync());
            }
            else
            {
                TempData["error"] = "Invalid!";
                return RedirectToAction("Login");

            }
            
        }
        public async Task<IActionResult> ServiceEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tp = await _context.Services.FindAsync(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ServiceEdit(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                _context.SaveChanges();
                TempData["success"] = "Service Updated Done!";
                return RedirectToAction("ServiceView");
            }
            return View(service);
        }

        

        [HttpGet]
        public IActionResult ServiceDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var tp = _context.Services.Find(id);
            if (tp == null)
            {
                return NotFound();
            }
            return View(tp);

        }
        [HttpPost, ActionName("ServiceDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult ServiceDeleteData(int? id)
        {
            var Nd = _context.Services.Find(id);
            if (Nd == null)
            {
                return NotFound();

            }
            _context.Services.Remove(Nd);
            _context.SaveChanges();
            TempData["success"] = "Service Data Deleted Done!";
            return RedirectToAction("ServiceView");
        }
        //----------------------End Service------------------------

        //----------------------------Report Part------------------

        //Citizen Report 
        public async Task<IActionResult> CitizenReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var us = _context.User.ToList();
                ViewBag.user = us.Count();

                return View(await _context.User.ToListAsync());
                
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
        //TrafficPolice Report
        public async Task<IActionResult> TpoliceReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
             var ra = _context.Trafficpolice.ToList();
            ViewBag.Trafficpolice = ra.Count();

            return View(await _context.Trafficpolice.ToListAsync());

            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
            
        }
        //
        public async Task<IActionResult> ReportFitnessRenew()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var rp = _context.Renewapplies.ToList();
                ViewBag.renew = rp.Count();
                return View(await _context.Renewapplies.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
        //accident
        public async Task<IActionResult> AccidentReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var ac = _context.AccidentCases.ToList();
                ViewBag.accident = ac.Count();

                return View(await _context.AccidentCases.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
       
        //Violation Report
        public async Task<IActionResult> ViolationReport()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var rv = _context.Rulesviolations.ToList();
                ViewBag.rulesviolation = rv.Count();

                return View(await _context.Rulesviolations.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
        //-------------------------------PDF---------------------------
        public async Task<IActionResult> ReportFitnessRenewPDF()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
              return View(await _context.Renewapplies.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
           
        }

        public async Task<IActionResult> ReportTpolicePDF()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var rv = _context.Rulesviolations.ToList();
                ViewBag.rulesviolation = rv.Count();

                return View(await _context.Trafficpolice.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
            
        }

        public async Task<IActionResult> ReportCitizenPDF()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                return View(await _context.User.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
           
        }

        public async Task<IActionResult> RulesViolationPDF()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var rv = _context.Rulesviolations.ToList();
                ViewBag.rulesviolation = rv.Count();

                return View(await _context.Rulesviolations.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }
            
        }
        public async Task<IActionResult> Accident()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                var rv = _context.Rulesviolations.ToList();
                ViewBag.rulesviolation = rv.Count();

                return View(await _context.AccidentCases.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }
        //------------------Add Vehicle--------------------------------------

        public IActionResult Vehicle()
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
        public async Task<IActionResult> AddVehicle(Vehicle vehicle)
        {
            if (!await _context.Vehicles.AnyAsync(x => x.Vehnum == vehicle.Vehnum))

            {
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                var allapp = _context.Vehicles.ToList();
                TempData["success"] = "Vehicle number addeds";
                return RedirectToAction("Vehiclelist");
            }
            TempData["error"] = "Vehicle number already registerd";

            return View(nameof(Vehicle));

        }

        public async Task<IActionResult> Vehiclelist()
        {
            ViewBag.abc = HttpContext.Session.GetInt32("UserId");

            if (ViewBag.abc != null && ViewBag.abc != 0)
            {
                return View(await _context.Vehicles.ToListAsync());
            }
            else
            {
                TempData["error"] = "Please Login First!";
                return RedirectToAction("Login");

            }

            
        }

        
    }
}