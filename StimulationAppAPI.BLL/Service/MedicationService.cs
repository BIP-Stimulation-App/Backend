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

        public Medication? GetById(int id)
        {
            return _context.Medications.Find(id);
        }

        public ICollection<Medication> GetMedications(string username)
        {
            return _context.Medications.Where(m => m.Dependence == username).ToArray();
        }
    }
}
