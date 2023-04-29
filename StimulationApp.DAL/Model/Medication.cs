using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model
{
    public enum MedicationFrequency {Once, Daily,Weekly}
    [Table("Medication")]
    public class Medication
    {
        [Key, Required ]
        public int Id { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string Name { get; set; } = null!;
        [Required, Column(TypeName = "varchar(100)")]
        public string Description { get; set; } = null!;

        [Required, Column(TypeName = "DATE")]
        public DateTime Time { get; set; } //exact day and time of next notification
        [Required, Column(TypeName = "smallint")]
        public MedicationFrequency Frequency { get; set; }
        [Required, Column(TypeName = "varchar(30)")]
        public string UserName { get; set; } = null!;
    }
}
