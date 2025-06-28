using FinalProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly MedicalHistoryRepository _medicalHistoryRepository;

        public MedicalHistoryController(AppDbContext context)
        {
            _medicalHistoryRepository = new MedicalHistoryRepository(context);
        }

        [HttpGet("medical-histories")]
        [Authorize(Roles = "Staff,Doctor")]
        public ActionResult<List<MedicalHistory>> GetAllMedicalHistories()
        {
            var histories = _medicalHistoryRepository.GetAllMedicalHistories();
            return Ok(histories);
        }

        [HttpGet("medical-histories/{id}")]
        [Authorize(Roles = "Staff,Doctor")]
        public ActionResult<MedicalHistory> GetMedicalHistoryById(int id)
        {
            var history = _medicalHistoryRepository.GetMedicalHistoryById(id);
            if (history == null)
            {
                return NotFound($"No medical history found with ID {id}");
            }
            return Ok(history);
        }

        [HttpPost("medical-histories")]
        //[Authorize(Roles = "Staff,Patient")]
        public ActionResult AddMedicalHistory([FromBody] MedicalHistory medicalHistory)
        {
            _medicalHistoryRepository.AddMedicalHistory(medicalHistory);
            return CreatedAtAction(nameof(GetMedicalHistoryById), new { id = medicalHistory.HistoryId }, medicalHistory);
        }

        [HttpPut("medical-histories/{id}")]
        [Authorize(Roles = "Staff,Patient")]
        public ActionResult UpdateMedicalHistory(int id, [FromBody] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.HistoryId)
            {
                return BadRequest("Medical history ID mismatch");
            }

            _medicalHistoryRepository.UpdateMedicalHistory(medicalHistory);
            return NoContent();
        }

        [HttpDelete("medical-histories/{id}")]
        [Authorize(Roles = "Staff,Patient")]
        public ActionResult DeleteMedicalHistory(int id)
        {
            var existingHistory = _medicalHistoryRepository.GetMedicalHistoryById(id);
            if (existingHistory == null)
            {
                return NotFound($"No medical history found with ID {id}");
            }

            _medicalHistoryRepository.DeleteMedicalHistory(id);
            return NoContent();
        }
        
       
    }

}
