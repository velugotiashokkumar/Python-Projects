﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Dto
{
    public class slotDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SlotDuration { get; set; }
        public string DailyStartTime { get; set; }
        public string DailyEndTime { get; set; }
        public int DoctorId { get; set; }
    }
}
