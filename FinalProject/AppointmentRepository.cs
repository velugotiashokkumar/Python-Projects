using FinalProject.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class AppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }

        public void AddAppointment(AppointmentDto appointmentDto)
        {
            using var transaction = _context.Database.BeginTransaction(); // ✅ Ensure transaction consistency

            try
            {
                // ✅ Create new appointment entry
                var appointment = new Appointment
                {
                    PatientId = appointmentDto.PatientId,
                    DoctorId = appointmentDto.DoctorId,
                    SlotId = appointmentDto.SlotId,
                    AppointmentDate = appointmentDto.AppointmentDate,
                    Status = appointmentDto.Status ?? "Booked"
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges(); // ✅ Save appointment first

                // ✅ Retrieve the newly created Appointment ID
                int appointmentId = appointment.AppointmentId;
                Console.WriteLine($"New Appointment ID: {appointmentId}");

                // ✅ Ensure the appointment ID is valid before updating slot
                if (appointmentId <= 0)
                {
                    throw new Exception("Failed to retrieve valid appointment ID.");
                }

                // ✅ Retrieve slot and update values
                var slot = _context.Slots.FirstOrDefault(s => s.SlotId == appointmentDto.SlotId);

                if (slot != null)
                {
                    slot.IsBooked = true;  // ✅ Mark slot as booked
                    slot.PatientId = appointmentDto.PatientId;  // ✅ Assign Patient ID
                    slot.AppointmentId = appointmentId;  // ✅ Link Appointment ID to Slot

                    _context.Entry(slot).State = EntityState.Modified;  // ✅ Force Entity Framework to track changes
                    _context.SaveChanges(); // ✅ Save slot updates

                    Console.WriteLine($"Slot {slot.SlotId} updated successfully: IsBooked={slot.IsBooked}, PatientId={slot.PatientId}, AppointmentId={slot.AppointmentId}");
                }
                else
                {
                    Console.WriteLine($"Slot with ID {appointmentDto.SlotId} not found.");
                }

                transaction.Commit(); // ✅ Ensure all updates are committed
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // ❌ Rollback changes if an error occurs
                Console.WriteLine($"Error booking appointment: {ex.Message}");
                throw;
            }
        }




        public void UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }

        public List<Appointment> GetAppointmentsByDoctorAndDate(int doctorId, DateTime date)
        {
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
                .ToList();
        }

        public List<Doctor> GetDoctorsBySpecializationAsync(string specialization)
        {
            return _context.Doctors
                .Where(d => d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public List<Appointment> GetTodaysAppointmentsByDoctor(int doctorId)
        {
            DateTime today = DateTime.Today; // Get today's date without time
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == today)
                .ToList();
        }
        public List<AppointmentDto> GetTodaysAppointments()
        {
            var today = DateTime.Today;

            return _context.Appointments
                .Where(a => a.AppointmentDate.Date == today)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    SlotId = a.SlotId,
                    AppointmentDate = a.AppointmentDate
                })
                .ToList();
        }
        public void CancelAppointment(int appointmentId)
        {
            using var transaction = _context.Database.BeginTransaction(); // ✅ Ensure consistency

            try
            {
                // ✅ Retrieve the appointment
                var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
                if (appointment == null)
                {
                    throw new Exception("Appointment not found.");
                }

                Console.WriteLine($"Cancelling appointment: {appointmentId}");

                // ✅ Retrieve the associated slot and reset its values
                var slot = _context.Slots.FirstOrDefault(s => s.SlotId == appointment.SlotId);
                if (slot != null)
                {
                    slot.IsBooked = false;  // ✅ Mark slot as available
                    slot.PatientId = null;  // ✅ Remove Patient ID
                    slot.AppointmentId = 0;  // ✅ Unlink Appointment ID

                    _context.Entry(slot).State = EntityState.Modified; // ✅ Ensure EF Core tracks update
                    _context.SaveChanges();

                    Console.WriteLine($"Slot {slot.SlotId} is now available.");
                }
                else
                {
                    Console.WriteLine("No associated slot found.");
                }

                // ✅ Remove appointment from database
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();

                transaction.Commit(); // ✅ Commit changes
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // ❌ Rollback if error occurs
                Console.WriteLine($"Error cancelling appointment: {ex.Message}");
                throw;
            }
        }
        //public List<Appointment> GetTodaysAppointments(int doctorId)
        //{
        //    DateTime startOfDay = DateTime.Today;
        //    DateTime endOfDay = DateTime.Today.AddDays(1).AddTicks(-1); // ✅ Covers full day

        //    var appointments = _context.Appointments
        //        .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= startOfDay && a.AppointmentDate <= endOfDay)
        //        .ToList();

        //    return appointments;
        //}

        public List<Appointment> GetPastAppointmentsByPatientId(int patientId)
        {
            return _context.Appointments
                .Where(a => a.PatientId == patientId && a.AppointmentDate < DateTime.Now)
                .Include(a => a.Doctor) // Include related doctor details
                .Include(a => a.Slot)   // Include slot details
                .ToList();
        }
        public List<AppointmentDto> GetUpcomingAppointments(int patientId)
        {
            return _context.Appointments
                .Where(a => a.PatientId == patientId && a.AppointmentDate >= DateTime.MinValue)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    SlotId = a.SlotId,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status
                })
                .ToList();
        }


    }


}
