using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StimulationAppAPI.BLL.Interface;
using StimulationAppAPI.DAL.Context;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.BLL.Service
{
    public class MedicationService:IMedicationService
    {
        private readonly UserContext _context;

        public MedicationService(UserContext context)
        {
            _context = context;
        }

        public Medication AddMedication(Medication medication)
        {
            _context.Medications.Add(medication);
            _context.SaveChanges();
            return medication;
        }

        public List<Medication> GetUserMedications(string username)
        {
            return _context.Medications.Where(m => m.UserName == username).ToList();
        }

        public Medication? GetById(int id)
        {
            return _context.Medications.Find(id);
        }

        public Medication? RemoveMedication(int id)
        {
            var result = GetById(id);
            if (result is null)
            {
                return null;
            }
            _context.Remove(result);
            _context.SaveChanges();
            return result;
        }
    }
}
