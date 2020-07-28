using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundHostedService.Model
{
    public class BackgroundJobs
    {
        [Key]
        public long JobID { get; set; }
        public DateTime JobTimeStramp { get; set; }
        public long JobDuration { get; set; }
        public string Status { get; set; }
    }

    //public enum JobStatus { 
    //    NotStarted,
    //    InProgress,
    //    Completed
    //}

    public class JobConstants {
        public const string Status_NOTStarted = "Not Started";
        public const string Status_Inprogress = "Inprogress";
        public const string Status_Completed = "Completed";
    }

}
