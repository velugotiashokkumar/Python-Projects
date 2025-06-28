using FinalProject.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class MedicalHistoryRepository
    {
        private readonly AppDbContext _context;
       

        public MedicalHistoryRepository(AppDbContext context)
        {
            _context = context;
            
        }

        public List<MedicalHistory> GetAllMedicalHistories()
        {
            return _context.MedicalHistories.ToList();
        }

        public MedicalHistory GetMedicalHistoryById(int id)
        {
            return _context.MedicalHistories.Find(id);
        }

        public void AddMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.MedicalHistories.Add(medicalHistory);
            _context.SaveChanges();
        }

        public void UpdateMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.MedicalHistories.Update(medicalHistory);
            _context.SaveChanges();
        }

        public void DeleteMedicalHistory(int id)
        {
            var history = _context.MedicalHistories.Find(id);
            if (history != null)
            {
                _context.MedicalHistories.Remove(history);
                _context.SaveChanges();
            }
        }
       

    }

}
