using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Dto
{
    public class MedicalHistoryDto
    {
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime? DateRecorded { get; set; }
    }
}
