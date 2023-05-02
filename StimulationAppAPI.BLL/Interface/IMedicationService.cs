using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.BLL.Interface
{
    public interface IMedicationService
    {
        public Medication? GetById(int id);
        public ICollection<Medication> GetMedications(string username);

    }
}
