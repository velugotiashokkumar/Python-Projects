using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FinalProject.Dto;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly SlotRepository _repository;

        // Constructor to inject the repository
        public SlotController(SlotRepository repository)
        {
            _repository = repository;
        }

        // Generate slots for a doctor's availability
        [HttpPost("GenerateSlots")]
        //[Authorize(Roles = "Staff")]
        public IActionResult GenerateSlots([FromBody] slotDto slotDto)
        {
            try
            {
                // Validate input before processing
                if (slotDto == null || string.IsNullOrEmpty(slotDto.StartDate) || string.IsNullOrEmpty(slotDto.EndDate) || slotDto.DoctorId == 0)
                {
                    return BadRequest(new { message = "Invalid slot data. Please provide all required fields." });
                }

                // Convert string inputs to DateTime & TimeSpan for processing
                DateTime startDate = DateTime.Parse(slotDto.StartDate);
                DateTime endDate = DateTime.Parse(slotDto.EndDate);
                TimeSpan slotDuration = TimeSpan.Parse(slotDto.SlotDuration);
                TimeSpan dailyStartTime = TimeSpan.Parse(slotDto.DailyStartTime);
                TimeSpan dailyEndTime = TimeSpan.Parse(slotDto.DailyEndTime);
                int doctorId = slotDto.DoctorId;

                // Call repository method to generate slots
                var slots = _repository.GenerateSlots(startDate, endDate, slotDuration, dailyStartTime, dailyEndTime, doctorId);

                return Ok(new { message = "Slots generated successfully!", slots });
            }
            catch (FormatException ex) // Handle invalid date/time formats
            {
                return BadRequest(new { message = "Invalid date or time format.", details = ex.Message });
            }
            catch (ArgumentException ex) // Handle logical validation errors
            {
                return BadRequest(new { message = "Validation error.", details = ex.Message });
            }
            catch (Exception ex) // Handle unexpected errors
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }


        [HttpDelete("delete-before-today/{doctorId}")]
        [Authorize(Roles = "Staff,Patient")]
        public IActionResult DeleteSlotsBeforeToday(int doctorId)
        {
            try
            {
                // Call the repository method to delete slots
                _repository.DeleteSlotsBeforeToday(doctorId);

                return Ok(new { Message = "All slots before today for the specified doctor have been deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while deleting slots.", Details = ex.Message });
            }
        }
    

        [HttpGet("UnbookedSlots/{doctorId}")]
        [Authorize(Roles = "Patient,Staff")]
        public ActionResult<List<Slot>> GetUnbookedSlotsByDoctor(int doctorId)
        {
            try
            {
                var unbookedSlots = _repository.GetUnbookedSlotsByDoctor(doctorId);

                if (unbookedSlots == null || unbookedSlots.Count == 0)
                {
                    return NotFound("No unbooked slots found for the given doctor.");
                }

                return Ok(unbookedSlots);
            }
            catch (Exception ex)
            {
                // Log the exception (if necessary)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles ="Staff,Patient")]
        public IActionResult GetSlotById(int id)
        {
            try
            {
                var slot = _repository.GetSlotById(id);

                if (slot == null)
                {
                    return NotFound(new { Message = $"No slot found with ID {id}." });
                }

                return Ok(slot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while retrieving the slot.", Details = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles ="Patient,Staff")]
        public IActionResult UpdateSlot(Slot slot)
        {
            try
            {
                _repository.UpdateSlot(slot);
                return Ok(new { Message = "Slot updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while updating the slot.", Details = ex.Message });
            }
        }
        [HttpGet("available")]
        public ActionResult<List<Slot>> GetUnbookedSlotsByDoctorAndDate([FromQuery] int doctorId, [FromQuery] DateTime date)
        {
            var slots = _repository.GetUnbookedSlotsByDoctorAndDate(doctorId, date);

            if (slots == null || slots.Count == 0)
            {
                return NotFound($"No available slots found for Doctor ID {doctorId} on {date:yyyy-MM-dd}.");
            }

            return Ok(slots);
        }



    }

}

