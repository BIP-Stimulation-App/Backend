using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimulationAppAPI.DAL.Model
{
    public class Medication
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime Next { get; set; } //exact day and time of next notification
        public TimeSpan Frequency { get; set; } //how many minutes, hours etc shoudl be added after repeat
        public string FrequencyText { get; set; }
    }
}
