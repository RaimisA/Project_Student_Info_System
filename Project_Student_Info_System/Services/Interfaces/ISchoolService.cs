using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Services.Interfaces
{
    public interface ISchoolService
    {
        void AddLecturesToDepartment(string departmentCode, IEnumerable<int> lectureIds);
        void AddStudentsToDepartment(string departmentCode, IEnumerable<int> studentNumbers);
        void CreateDepartment(Department department, IEnumerable<int> studentNumbers, IEnumerable<int> lectureIds);
        void CreateLecture(Lecture lecture, string departmentCode);
        void CreateStudent(Student student, string departmentCode, IEnumerable<int> lectureIds);
        IEnumerable<Department> GetAllDepartments();
        IEnumerable<Lecture> GetAllLectures();
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Lecture> GetLecturesByDepartment(string departmentCode);
        IEnumerable<Lecture> GetLecturesByStudent(int studentNumber);
        IEnumerable<Student> GetStudentsByDepartment(string departmentCode);
        void TransferStudent(int studentNumber, string newDepartmentCode, IEnumerable<int> newLectureIds);
    }
}