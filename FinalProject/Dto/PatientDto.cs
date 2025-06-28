using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string? PatientEmail { get; set; }
        public string PatientPhoneNumber { get; set; }
        public DateTime? PatientDateOfBirth { get; set; }
    }
}
