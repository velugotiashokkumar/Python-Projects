using FinalProject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class SlotRepository
    {
        private readonly AppDbContext _context;

        // Constructor to inject the DbContext
        public SlotRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Slot> GenerateSlots(

     DateTime startDate,

     DateTime endDate,

     TimeSpan slotDuration,

     TimeSpan dailyStartTime,

     TimeSpan dailyEndTime,

     int doctorId)

        {

            // Ensure dailyStartTime and dailyEndTime are valid

            if (dailyStartTime >= dailyEndTime)

            {

                throw new ArgumentException("Daily start time must be before daily end time.");

            }

            List<Slot> slots = new List<Slot>();

            // Loop through each date in the range

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))

            {

                for (var time = dailyStartTime; time < dailyEndTime; time = time.Add(slotDuration))

                {

                    var slot = new Slot

                    {

                        SlotDate = date,

                        StartTime = time,

                        EndTime = time.Add(slotDuration),

                        DoctorId = doctorId,

                        IsBooked = false // Initialize as unbooked

                    };

                    slots.Add(slot);

                    // Add the slot to the DbContext for persistence

                    _context.Slots.Add(slot);

                }

            }

            // Save the generated slots to the database

            _context.SaveChanges();

            return slots;

        }

        public void DeleteSlotsBeforeToday(int doctorId)
        {
            // Get the start of today
            DateTime today = DateTime.Today;

            // Find all slots for the doctor where SlotDate is less than today
            var slotsToDelete = _context.Slots
                                        .Where(slot => slot.DoctorId == doctorId && slot.SlotDate < today)
                                        .ToList();

            // Remove the found slots
            if (slotsToDelete.Any())
            {
                _context.Slots.RemoveRange(slotsToDelete);
                _context.SaveChanges(); // Save changes to the database
            }
        }


        // Method to retrieve all unbooked slots for a specific doctor
        public List<Slot> GetUnbookedSlotsByDoctor(int doctorId)
        {
            // Query the database to find slots with the given DoctorId where IsBooked is false
            var unbookedSlots = _context.Slots
                                        .Where(slot => slot.DoctorId == doctorId && slot.IsBooked == false)
                                        .ToList();

            return unbookedSlots;
        }
        public Slot GetSlotById(int id)
        {
            return _context.Slots.FirstOrDefault(s => s.SlotId == id);
        }

        public void UpdateSlot(Slot slot)
        {
            _context.Slots.Update(slot);
            _context.SaveChanges();
        }
        public List<Slot> GetUnbookedSlotsByDoctorAndDate(int doctorId, DateTime date)
        {
            return _context.Slots
                .Where(s => s.DoctorId == doctorId && s.SlotDate.Date == date.Date && !s.IsBooked)
                .ToList();
        }

    }

}
