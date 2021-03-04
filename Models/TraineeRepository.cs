using CoreProject_Raihan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public class TraineeRepository:ITraineeRepository
    {
        private readonly ApplicationDbContext db;
        public TraineeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Trainee Add(Trainee trainee)
        {
            db.Trainees.Add(trainee);
            db.SaveChanges();

            return trainee;
        }

        public Trainee Delete(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            if (trainee != null)
            {
                db.Trainees.Remove(trainee);
                db.SaveChanges();
            }
            return trainee;
        }

        public IEnumerable<Trainee> GetAll()
        {
            return db.Trainees;
        }

        public Trainee GetTrainee(int id)
        {
            return db.Trainees.Where(x => x.TraineeID == id).SingleOrDefault();
        }

        public Trainee Update(Trainee trainee)
        {
            db.Entry(trainee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return trainee;
        }
    }
}
