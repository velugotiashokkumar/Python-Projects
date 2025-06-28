using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string? DoctorEmail { get; set; }
        
        public string? DoctorContactNumber { get; set; }
        // Navigation Properties
        public List<Appointment>? Appointments { get; set; }
        public List<Slot>? Slots { get; set; }
    }
}
