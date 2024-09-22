using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Delete(int studentNumber);
        IEnumerable<Student> GetAll();
        Student GetById(int studentNumber);
        void Update(Student student);
    }
}