using Microsoft.EntityFrameworkCore;
using FinalProject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class DoctorRepository
    {
        private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;
        //private AppDbContext context;

        public DoctorRepository(AppDbContext context, UserRepository userRepository)
        {
            _context = context; // ✅ Prevents null injection
            _userRepository = userRepository;
        }

        //public DoctorRepository(AppDbContext context)
        //{
        //    this.context = context;
        //}

        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            return _context.Doctors.Find(id);
        }

        public void AddDoctor(DoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                DoctorId = doctorDto.DoctorId, // Assuming ID is provided, otherwise remove this line
                DoctorName = doctorDto.DoctorName,
                Specialization = doctorDto.Specialization,
                DoctorEmail = doctorDto.DoctorEmail,
                DoctorContactNumber = doctorDto.DoctorContactNumber
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            //if (_userRepository == null)
            //{
            //    throw new InvalidOperationException("_userRepository is not initialized.");
            //}
            _userRepository.RegisterUserForDoctor(doctorDto.DoctorEmail, doctorDto.DoctorContactNumber, doctor.DoctorId);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public void DeleteDoctor(int doctorId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var appointments = _context.Appointments.Where(a => a.DoctorId == doctorId).ToList();

                    if (appointments.Any())
                    {
                        foreach (var appointment in appointments)
                        {
                            appointment.Status = "Canceled"; // Update the appointment instead of deleting
                        }
                        _context.SaveChanges();
                    }

                    // Find and delete the doctor
                    var doctor = _context.Doctors.Find(doctorId);
                    if (doctor != null)
                    {
                        _context.Doctors.Remove(doctor);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error deleting doctor due to linked appointments.", ex);
                }
            }
        }

        //public List<Appointment> GetAppointmentsByDoctorAndDate(int doctorId, DateTime date)
        //{
        //    return _context.Appointments
        //        .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
        //        .ToList();
        //}

        public void CancelAppointmentsByDoctorAndDate(int doctorId, DateTime date)
        {
            var appointmentsToUpdate = _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date)
                .ToList(); // Retrieve the list of matching appointments

            if (appointmentsToUpdate.Any())
            {
                foreach (var appointment in appointmentsToUpdate)
                {
                    appointment.Status = "Canceled"; // Update only the Status property
                }

                _context.SaveChanges(); // Save changes to the database
            }
        }


        public List<Patient> GetPatientsByDiagnosis(string diagnosis)
        {
            return _context.MedicalHistories
                .AsEnumerable() // Forces client-side evaluation
                .Where(m => m.Diagnosis.Equals(diagnosis, StringComparison.OrdinalIgnoreCase))
                .Select(m => m.Patient)
                .ToList();
        }

        public List<DoctorDto> GetDoctorsWithSpecialization()
        {
            return _context.Doctors
                .Select(d => new DoctorDto
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.DoctorName,
                    Specialization = d.Specialization
                })
                .ToList();
        }


    }


}
