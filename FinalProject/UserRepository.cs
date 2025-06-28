using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

       

        // ✅ Register Patient as User with Custom Password
        public void RegisterUserForPatient(string email, string password, int patientId)
        {
            var user = new User
            {
                UserName = email,
                Password = password, // ✅ Now users provide their own password
                Role = "Patient",
                RoleId = patientId
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // ✅ Register Doctor as User with Custom Password
        public void RegisterUserForDoctor(string email, string password, int doctorId)
        {
            var user = new User
            {
                UserName = email,
                Password = password, // ✅ Now users provide their own password
                Role = "Doctor",
                RoleId = doctorId
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // ✅ Register Staff as User with Custom Password
        public void RegisterUserForStaff(string email, string password, int staffId)
        {
            var user = new User
            {
                UserName = email,
                Password = password, // ✅ Now users provide their own password
                Role = "Staff",
                RoleId = staffId
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}
