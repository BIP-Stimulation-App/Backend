using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model.Request
{
    public class NewMedication
    {
        public NewMedication()
        {
        }
        public NewMedication(Medication medication)
        {
            Name = medication.Name;
            Description = medication.Description;
            Time = medication.Time;
            Frequency = medication.Frequency;
        }

        [Required, Column(TypeName = "varchar(30)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; } = null!;

        [Required, Column(TypeName = "DATE")]
        public DateTime Time { get; set; } //exact day and time of next notification
        [Required, Column(TypeName = "smallint")]
        public MedicationFrequency Frequency { get; set; }
    }
}

