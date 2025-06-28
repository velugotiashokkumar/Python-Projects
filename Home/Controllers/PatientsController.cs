using FinalProject;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Dto;


namespace Home.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientRepository _patientRepository;

        public PatientsController(PatientRepository appointmentRepository)
        {
            _patientRepository = appointmentRepository;

        }

        [HttpGet("patients")]
        [Authorize]
        public ActionResult<List<Patient>> GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("patients/{id}")]
        [Authorize(Roles ="Patient,Staff")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            var patient = _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound($"No patient found with ID {id}");
            }
            return Ok(patient);
        }

        [HttpPost("patients")]
        //[Authorize(Roles = "Patient,Staff")]
        
        public IActionResult AddPatient([FromBody] PatientDto patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest("Invalid patient data.");
            }

            _patientRepository.AddPatient(patientDto);
            return Ok(new { message = "Patient added successfully" });
        }
        //public ActionResult AddPatient([FromBody] Patient patient)
        //{
        //    _patientRepository.AddPatient(patient);
        //    return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
        //}

        [HttpPut("patients/{id}")]
        [Authorize(Roles = "Patient,Staff")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            if (patientDto == null || patientDto.PatientId != id)
            {
                return BadRequest("Invalid patient data or mismatched ID.");
            }

            var updatedPatient = _patientRepository.UpdatePatient(patientDto);

            if (updatedPatient == null)
            {
                return NotFound("Patient not found.");
            }

            return Ok("Patient updated successfully.");
        }
        //public ActionResult UpdatePatient(int id, [FromBody] Patient patient)
        //{
        //    if (id != patient.PatientId)
        //    {
        //        return BadRequest("Patient ID mismatch");
        //    }

        //    _patientRepository.UpdatePatient(patient);
        //    return NoContent();
        //}

        [HttpDelete("patients/{id}")]
        [Authorize(Roles = "Patient,Staff")]
        public ActionResult DeletePatient(int id)
        {
            var existingPatient = _patientRepository.GetPatientById(id);
            if (existingPatient == null)
            {
                return NotFound($"No patient found with ID {id}");
            }

            _patientRepository.DeletePatient(id);
            return NoContent();
        }

        // API endpoint to get doctors by specialization
        // Asynchronous API endpoint to get doctors by specialization
        //[HttpGet("specialization/{specialization}")]
        //public async Task<ActionResult<List<Doctor>>> GetDoctorsBySpecializationAsync(string specialization)
        //{
        //    var doctors = await Task.Run(() => _patientRepository.GetDoctorsBySpecializationAsync(specialization)); // Simulate async if the method in repository is not async

        [HttpGet("medical-history/{phoneNumber}")]
        public IActionResult GetMedicalHistoryByPhoneNumber(string phoneNumber)
        {
            try
            {
                var medicalHistory = _patientRepository.GetMedicalHistoryByPhoneNumber(phoneNumber);
                return Ok(medicalHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error retrieving medical history: {ex.Message}" });
            }
        }



    }

}
