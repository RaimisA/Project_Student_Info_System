using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Repositories.Interfaces
{
    public interface ILectureRepository
    {
        void Add(Lecture lecture);
        void Delete(int lectureId);
        IEnumerable<Lecture> GetAll();
        Lecture GetById(int lectureId);
        void Update(Lecture lecture);
    }
}