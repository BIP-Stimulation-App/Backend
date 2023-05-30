using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace StimulationAppAPI.DAL.Model
{
    public enum Difficulty {Easy,Normal,Hard}
    public enum Category{Strength, Cardio, Yoga, Coordination,Walking}
    [Table("Exercise")]
    public class Exercise
    {
        [Key,Required, Column(TypeName = "int")]
        public int Id { get; set; }
        [Required, Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Required, Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        [Required, Column(TypeName = "time")]
        public TimeSpan Duration { get; set; } //is in seconds
        [Required, Column(TypeName = "smallint")]
        public Difficulty Difficulty { get; set; }
        [Required, Column(TypeName = "int")]
        public int Reward { get; set; }
        [Required, Column(TypeName = "smallint")]
        public Category Category { get; set; }
    }
}
