using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Services.Interfaces
{
    public interface IStudentService
    {
        void CreateStudent(Student student, string departmentCode, IEnumerable<int> lectureIds);
        void TransferStudent(int studentNumber, string newDepartmentCode, IEnumerable<int> newLectureIds);
        IEnumerable<Student> GetAllStudents();
    }
}