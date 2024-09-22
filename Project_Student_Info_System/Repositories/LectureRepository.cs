using Project_Student_Info_System.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Project_Student_Info_System.Database;
using Project_Student_Info_System.Repositories.Interfaces;

namespace Project_Student_Info_System.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly SchoolContext _context;

        public LectureRepository(SchoolContext context)
        {
            _context = context;
        }

        public Lecture GetById(int lectureId)
        {
            return _context.Lectures
                .Include(l => l.Departments)
                .Include(l => l.Students)
                .FirstOrDefault(l => l.LectureId == lectureId);
        }

        public IEnumerable<Lecture> GetAll()
        {
            return _context.Lectures
                .Include(l => l.Departments)
                .Include(l => l.Students)
                .ToList();
        }

        public void Add(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
        }

        public void Update(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
            _context.SaveChanges();
        }

        public void Delete(int lectureId)
        {
            var lecture = GetById(lectureId);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                _context.SaveChanges();
            }
        }
    }
}
