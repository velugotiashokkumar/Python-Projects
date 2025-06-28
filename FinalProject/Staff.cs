using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRole { get; set; } // E.g., Receptionist, Nurse, Admin
        public string? StaffEmail { get; set; }
        public string StaffPhoneNumber { get; set; }

    }
}
