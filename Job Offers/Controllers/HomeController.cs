using Job_Offers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job_Offers.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); //db inst
        public ActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list);
        }

        //add details page
        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if(job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = job.Id;  // to save the jobId 
            return View(job);
        }


        [Authorize]
        public ActionResult Applay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Applay(string Message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobID"];

            var check = db.ApplayForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList(); //check if the user already applied
            if (check.Count < 1)
            {
                var job = new ApplayForJob();
                job.UserId = userId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApplayTiem = DateTime.Now;

                db.ApplayForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "لقد تم التقديم بنجاح";
            } else
            {
                ViewBag.Result = "لقد سبق وتقدمت له لهذه الوظيفه";
            }
            return View();
        }


        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplayForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}