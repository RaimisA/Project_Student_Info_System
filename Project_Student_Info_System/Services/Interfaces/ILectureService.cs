using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Services.Interfaces
{
    public interface ILectureService
    {
        void CreateLecture(Lecture lecture, string departmentCode);
        IEnumerable<Lecture> GetAllLectures();
        IEnumerable<Lecture> GetLecturesByStudent(int studentNumber);
    }
}