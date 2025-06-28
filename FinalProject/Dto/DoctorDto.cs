using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Dto
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string? DoctorEmail { get; set; }

        public string? DoctorContactNumber { get; set; }
    }
}
