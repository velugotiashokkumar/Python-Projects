using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class StaffRepository
    {
        
            private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;

        // Constructor to inject the DbContext
        public StaffRepository(AppDbContext context, UserRepository userRepository)
            {
                _context = context;
            _userRepository = userRepository;
        }

            // Create - Add a new staff member
            public void AddStaff(Staff staff)
            {
                _context.Staffs.Add(staff);
                _context.SaveChanges();
            _userRepository.RegisterUserForStaff(staff.StaffEmail, "default_password", staff.StaffId);// Commit changes to the database
            }

            // Read - Get all staff members
            public List<Staff> GetAllStaff()
            {
                return _context.Staffs.ToList();
            }

            // Read - Get staff member by ID
            public Staff? GetStaffById(int staffId)
            {
                return _context.Staffs.FirstOrDefault(s => s.StaffId == staffId);
            }

            // Update - Modify an existing staff member
            public void UpdateStaff(Staff staff)
            {
                var existingStaff = _context.Staffs.FirstOrDefault(s => s.StaffId == staff.StaffId);
                if (existingStaff != null)
                {
                    existingStaff.StaffName = staff.StaffName;
                    existingStaff.StaffRole = staff.StaffRole;
                    existingStaff.StaffEmail = staff.StaffEmail;
                    existingStaff.StaffPhoneNumber = staff.StaffPhoneNumber;

                    _context.SaveChanges(); // Commit changes to the database
                }
            }

            // Delete - Remove a staff member by ID
            public void DeleteStaff(int staffId)
            {
                var staff = _context.Staffs.FirstOrDefault(s => s.StaffId == staffId);
                if (staff != null)
                {
                    _context.Staffs.Remove(staff);
                    _context.SaveChanges(); // Commit changes to the database
                }
            }
    }
}
