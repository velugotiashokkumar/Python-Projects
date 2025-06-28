using FinalProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Home.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorController(DoctorRepository appointmentRepository)
        {
            _doctorRepository = appointmentRepository;
            
        }


        [HttpGet("doctors")]
        //[Authorize(Roles ="Staff,Patient")]
        public ActionResult<List<Doctor>> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("doctors/{id}")]
        [Authorize(Roles = "Staff,Patient")]

        public ActionResult<Doctor> GetDoctorById(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound($"No doctor found with ID {id}");
            }
            return Ok(doctor);
        }

        [HttpPost("doctors")]
        //[Authorize(Roles = "Staff")]
        public ActionResult AddDoctor([FromBody] DoctorDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest(new { message = "Invalid data" });
            }

            _doctorRepository.AddDoctor(doctorDto);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctorDto.DoctorId }, doctorDto);
        }

        [HttpPut("doctors/{id}")]
        //[Authorize(Roles = "Staff,Doctor")]
        public ActionResult UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return BadRequest("Doctor ID mismatch");
            }

            _doctorRepository.UpdateDoctor(doctor);
            return NoContent();
        }

        [HttpDelete("doctors/{id}")]
        //[Authorize(Roles = "Staff")]
        public ActionResult DeleteDoctor(int id)
        {
            var existingDoctor = _doctorRepository.GetDoctorById(id);
            if (existingDoctor == null)
            {
                return NotFound($"No doctor found with ID {id}");
            }

            _doctorRepository.DeleteDoctor(id);
            return NoContent();
        }

        //[HttpGet("appointments/{doctorId}/{date}")]
        //public ActionResult<List<Appointment>> GetAppointmentsByDoctorAndDate(int doctorId, DateTime date)
        //{
        //    var appointments = _doctorRepository.GetAppointmentsByDoctorAndDate(doctorId, date);
        //    if (appointments == null || !appointments.Any())
        //    {
        //        return NotFound($"No appointments found for doctor ID {doctorId} on {date:yyyy-MM-dd}");
        //    }
        //    return Ok(appointments);
        //}

        [HttpPut("cancel/{doctorId}/{date}")]
        [Authorize(Roles = "Staff,Doctor")]
        public IActionResult CancelAppointments(int doctorId, DateTime date)
        {
            try
            {
                _doctorRepository.CancelAppointmentsByDoctorAndDate(doctorId, date); // Synchronous repository method
                return Ok($"Appointments for doctor ID {doctorId} on {date.Date.ToShortDateString()} have been marked as canceled.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("patients/diagnosis/{diagnosis}")]
        [Authorize(Roles = "Staff,Doctor")]
        public ActionResult<List<Patient>> GetPatientsByDiagnosis(string diagnosis)
        {
            var patients = _doctorRepository.GetPatientsByDiagnosis(diagnosis);
            if (patients == null || !patients.Any())
            {
                return NotFound($"No patients found with diagnosis {diagnosis}");
            }
            return Ok(patients);
        }
        [HttpGet]
        //[Authorize(Roles = "Staff,Patient")]
        public ActionResult<List<DoctorDto>> GetAllDoctorsWithSpecialization()
        {
            var doctors = _doctorRepository.GetDoctorsWithSpecialization();
            if (doctors == null || doctors.Count == 0)
            {
                return NotFound("No doctors found.");
            }
            return Ok(doctors);
        }

    }

}
