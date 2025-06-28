using FinalProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffRepository _staffRepository;

        // Constructor to inject StaffRepository
        public StaffController(StaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // Create - Add a new staff member
        [HttpPost("register-staff")]
        public IActionResult RegisterStaff([FromBody] Staff staff)
        {
            try
            {
                if (staff == null || string.IsNullOrEmpty(staff.StaffEmail))
                {
                    return BadRequest("Invalid staff data.");
                }

                // Call repository method to add the staff
                _staffRepository.AddStaff(staff);

                return Ok(new { message = "Staff registered successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error registering staff: {ex.Message}");
            }
        }

        // Read - Get all staff members
        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAllStaff()
        {
            try
            {
                var staffList = _staffRepository.GetAllStaff();
                return Ok(staffList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // Read - Get a staff member by ID
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetStaffById(int id)
        {
            try
            {
                var staff = _staffRepository.GetStaffById(id);
                if (staff == null)
                {
                    return NotFound(new { Message = $"No staff found with ID {id}" });
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // Update - Modify an existing staff member
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Staff,Admin")]
        public IActionResult UpdateStaff(int id, [FromBody] Staff staff)
        {
            try
            {
                if (id != staff.StaffId)
                {
                    return BadRequest(new { Message = "Staff ID mismatch" });
                }
                _staffRepository.UpdateStaff(staff);
                return Ok(new { Message = "Staff updated successfully.", Staff = staff });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // Delete - Remove a staff member by ID
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStaff(int id)
        {
            try
            {
                var existingStaff = _staffRepository.GetStaffById(id);
                if (existingStaff == null)
                {
                    return NotFound(new { Message = $"No staff found with ID {id}" });
                }
                _staffRepository.DeleteStaff(id);
                return Ok(new { Message = "Staff deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
