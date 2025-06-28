using FinalProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Dto;
using Microsoft.AspNetCore.Authorization;



namespace Home.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly SlotRepository _slotRepository;
        public AppointmentController(AppointmentRepository appointmentRepository, SlotRepository slotRepository)
        {
            _appointmentRepository = appointmentRepository;
            _slotRepository = slotRepository;
        }

        [HttpGet("appointments")]
        [Authorize(Roles ="Staff,Doctor")]
        public ActionResult<List<Appointment>> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("appointments/{id}")]
        [Authorize(Roles = "Staff,Doctor")]
        public ActionResult<Appointment> GetAppointmentById(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound($"No appointment found with ID {id}");
            }
            return Ok(appointment);
        }

        [HttpPost("appointments")]
        //[Authorize(Roles = "Patient,Staff")]
        public IActionResult AddAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest(new { error = "Invalid request. AppointmentDto is required." });
            }

            try
            {
                _appointmentRepository.AddAppointment(appointmentDto);

                // ✅ Return JSON response instead of plain text
                return Ok(new { message = "Appointment booked successfully!", appointmentId = appointmentDto.AppointmentId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error booking appointment: {ex.Message}" });
            }

            //// Return a response
            //return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
        }


        //public ActionResult AddAppointment([FromBody] AppointmentDto newAppointment)
        //{
        //    if(newAppointment != null)
        //    {
        //        return BadRequest("Appointment is null");
        //    }
        //    var appointment = new Appointment
        //    {
        //        AppointmentId = newAppointment.AppointmentId,
        //        SlotId = newAppointment.SlotId,
        //        AppointmentDate = newAppointment.AppointmentDate

        //    };
        //    _appointmentRepository.AddAppointment(appointment);
        //    return Ok(appointment);

        //}
        [HttpGet("specialization/{specialization}")]
        [Authorize(Roles = "Patient,Staff")]
        public async Task<ActionResult<List<Doctor>>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var doctors = await Task.Run(() => _appointmentRepository.GetDoctorsBySpecializationAsync(specialization)); // Simulate async if the method in repository is not async

            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found with specialization '{specialization}'.");
            }

            return Ok(doctors);
        }


        [HttpPut("appointments/{id}")]
        [Authorize(Roles = "Staff,Patient")]
        public ActionResult UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest("Appointment ID mismatch");
            }

            _appointmentRepository.UpdateAppointment(appointment);
            return NoContent();
        }

        //[HttpPut("appointments/update-range")]
        //[Authorize]
        //public ActionResult UpdateAppointments([FromBody] List<Appointment> appointments)
        //{
        //    _appointmentRepository.UpdateAppointments(appointments);
        //    return NoContent();
        //}

        [HttpDelete("appointments/{id}")]
        [Authorize(Roles = "Staff,Patient")]
        public ActionResult DeleteAppointment(int id)
        {
            var existingAppointment = _appointmentRepository.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                return NotFound($"No appointment found with ID {id}");
            }

            _appointmentRepository.DeleteAppointment(id);
            return NoContent();
        }

        [HttpGet("appointments/doctor/{doctorId}/date/{date}")]
        [Authorize(Roles = "Staff,Doctor")]
        public ActionResult<List<Appointment>> GetAppointmentsByDoctorAndDate(int doctorId, DateTime date)
        {
            var appointments = _appointmentRepository.GetAppointmentsByDoctorAndDate(doctorId, date);
            if (appointments == null || !appointments.Any())
            {
                return NotFound($"No appointments found for doctor ID {doctorId} on {date:yyyy-MM-dd}");
            }
            return Ok(appointments);
        }
        [HttpPut("appointments/{id}/cancel")]
        [Authorize(Roles = "Patient,Staff")]
        public ActionResult CancelAppointment(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound($"No appointment found with ID {id}");
            }

            // Get the related slot
            var slot = _slotRepository.GetSlotById(appointment.SlotId);
            if (slot == null)
            {
                return BadRequest("Slot is invalid or does not exist.");
            }

            // Dynamically calculate the slot's start date and time
            var slotStartDateTime = appointment.AppointmentDate.Date.Add(slot.StartTime);
            if (slotStartDateTime >= DateTime.Now)
            {
                return BadRequest("Cannot cancel the appointment as the slot start time has passed.");
            }

            // Mark the appointment as cancelled
            appointment.Status = "Cancelled"; // Assuming you add this property to the Appointment model
            _appointmentRepository.UpdateAppointment(appointment);

            // Update the slot to make it available
            slot.IsBooked = false; // Assuming IsBooked is an integer or boolean
            _slotRepository.UpdateSlot(slot);

            return Ok(new { Message = "Appointment cancelled successfully and slot is now available for booking." });
        }






        [HttpGet("todays-appointments/{doctorId}")]
        public IActionResult GetTodaysAppointments(int doctorId)
        {
            try
            {
                var appointments = _appointmentRepository.GetTodaysAppointmentsByDoctor(doctorId);

                if (appointments == null || appointments.Count == 0)
                {
                    return NotFound(new { message = "No appointments found for this doctor today." });
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error fetching appointments: {ex.Message}" });
            }
        }

        [HttpGet("today")]
        public ActionResult<List<AppointmentDto>> GetTodaysAppointments()
        {
            var appointments = _appointmentRepository.GetTodaysAppointments();
            
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found for today.");
            }

            return Ok(appointments);
        }

        [HttpDelete("cancel/{appointmentId}")]
        public IActionResult CancelApointment(int appointmentId)
        {
            try
            {
                _appointmentRepository.CancelAppointment(appointmentId);
                return Ok(new { message = "Appointment cancelled successfully, slot is now available for booking!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error cancelling appointment: {ex.Message}" });
            }
        }
        [HttpGet("past/{patientId}")]
        public ActionResult<List<Appointment>> GetPastAppointments(int patientId)
        {
            var appointments = _appointmentRepository.GetPastAppointmentsByPatientId(patientId);

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound(new { message = "No past appointments found for this patient." });
            }

            return Ok(appointments);
        }
        [HttpGet("upcoming/{patientId}")]
        public IActionResult GetUpcomingAppointments(int patientId)
        {
            var appointments = _appointmentRepository.GetUpcomingAppointments(patientId);

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No upcoming appointments found.");
            }

            return Ok(appointments);
        }


    }



}



