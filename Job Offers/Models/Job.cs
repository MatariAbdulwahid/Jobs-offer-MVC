using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Offers.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم الوظيفه")]
        public string JobTitle { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفه")]
        public string JobContent { get; set; }
       
        [Display(Name = "صورة الوظيفه")]
        public string  JobImage { get; set; }
        [Display(Name = "نوع الوظيفه")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ApplayForJob> ApplyJobs { get; set; }
    }
}