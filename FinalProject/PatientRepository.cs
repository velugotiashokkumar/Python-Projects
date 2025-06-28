using FinalProject.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class PatientRepository
    {
        private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;
       

        public PatientRepository(AppDbContext context,UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        //public PatientRepository(AppDbContext context)
        //{
        //    this.context = context;
        //}

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientById(int id)
        {
            return _context.Patients.Find(id);
        }

        //public void AddPatient(Patient patient)
        //{
        //    _context.Patients.Add(patient);
        //    _context.SaveChanges();
        //}
        public void AddPatient(PatientDto patientDto)
        {
            var patient = new Patient
            {
                PatientId = patientDto.PatientId,
                PatientName = patientDto.PatientName,
                PatientEmail = patientDto.PatientEmail,
                PatientPhoneNumber = patientDto.PatientPhoneNumber,
                PatientDateOfBirth = patientDto.PatientDateOfBirth
            };

            _context.Patients.Add(patient);
            _context.SaveChanges();
            _userRepository.RegisterUserForPatient(patientDto.PatientEmail, "default_password", patient.PatientId);
        }

        //public void UpdatePatient(Patient patient)
        //{
        //    _context.Patients.Update(patient);
        //    _context.SaveChanges();
        //}
        public Patient UpdatePatient(PatientDto patientDto)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientDto.PatientId);
            if (patient == null) return null;

            patient.PatientName = patientDto.PatientName;
            patient.PatientEmail = patientDto.PatientEmail;
            patient.PatientPhoneNumber = patientDto.PatientPhoneNumber;
            patient.PatientDateOfBirth = patientDto.PatientDateOfBirth;

            _context.SaveChanges();
            return patient;
        }

        public void DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }
        public List<MedicalHistoryDto> GetMedicalHistoryByPhoneNumber(string phoneNumber)
        {
            // ✅ Retrieve patient based on phone number
            var patient = _context.Patients.FirstOrDefault(p => p.PatientPhoneNumber == phoneNumber);

            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            // ✅ Retrieve medical history using PatientId
            var medicalHistory = _context.MedicalHistories
                .Where(mh => mh.PatientId == patient.PatientId)
                .Select(mh => new MedicalHistoryDto
                {
                    HistoryId = mh.HistoryId,
                    PatientId = mh.PatientId,
                    Diagnosis = mh.Diagnosis,
                    Treatment = mh.Treatment,
                    DateRecorded = mh.DateRecorded
                })
                .ToList();

            return medicalHistory;
        }


       
    }

}
