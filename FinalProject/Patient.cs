using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string? PatientEmail { get; set; }
        public string PatientPhoneNumber { get; set; }
        public DateTime? PatientDateOfBirth { get; set; }
        // Navigation Properties
        public List<Appointment>? Appointments { get; set; }
        public List<MedicalHistory>? MedicalHistories { get; set; }
        public List<Slot>? BookedSlots { get; set; }
    }
}
