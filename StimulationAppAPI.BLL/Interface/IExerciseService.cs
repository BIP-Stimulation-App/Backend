using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StimulationAppAPI.DAL.Model;

namespace StimulationAppAPI.BLL.Interface
{
    public interface IExerciseService
    {
        public Exercise AddExercise(Exercise exercise);
        public Exercise? RemoveExercise(Exercise exercise);
        public Exercise? GetExercise(int id);
        public List<Exercise>? GetExercises();
        public Exercise? UpdateExercise(Exercise exercise);
    }
}
