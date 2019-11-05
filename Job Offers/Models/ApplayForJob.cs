using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Offers.Models
{

    //n to n
    public class ApplayForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplayTiem { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}