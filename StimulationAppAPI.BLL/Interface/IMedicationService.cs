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
        public Medication AddMedication(Medication medication);
        public List<Medication> GetUserMedications(string username);
        public Medication? GetById(int id);
        public Medication? RemoveMedication(int id);

    }
}
