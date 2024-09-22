using Project_Student_Info_System.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Project_Student_Info_System.Database;
using Project_Student_Info_System.Repositories.Interfaces;

namespace Project_Student_Info_System.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student GetById(int studentNumber)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Lectures)
                .FirstOrDefault(s => s.StudentNumber == studentNumber);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Lectures)
                .ToList();
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int studentNumber)
        {
            var student = GetById(studentNumber);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
