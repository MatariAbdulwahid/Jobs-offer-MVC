using Job_Offers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("Email", "Password"); // sender email , password
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("Email"));
            mail.Subject = contact.Subject;
            //mail.IsBodyHtml = true;

            mail.Body = contact.Message;

            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }





        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            ||a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }
    }
}