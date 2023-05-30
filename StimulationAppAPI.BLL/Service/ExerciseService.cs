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
    public class ExerciseService:IExerciseService
    {
        private UserContext _context;
        public ExerciseService(UserContext context)
        {
            _context = context;
        }
        public Exercise AddExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
            return exercise;
        }

        public Exercise? RemoveExercise(Exercise exercise)
        {
            _context.Remove(exercise);
            _context.SaveChanges();
            return exercise;
        }

        public Exercise? GetExercise(int id)
        {
            return _context.Exercises.Find(id);
        }

        public List<Exercise>? GetExercises()
        {
            return _context.Exercises.ToList();
        }

        public Exercise? UpdateExercise(Exercise exercise)
        {
            var newExercise =_context.Exercises.Update(exercise).Entity;
            _context.SaveChanges();
            return newExercise;
        }
    }
}
