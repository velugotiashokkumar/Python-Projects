using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public  string Password { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }  // Can be PatientId, DoctorId, or StaffId

        // ✅ Navigation Properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Staff? Staff { get; set; }


    }
}
