using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Services.Interfaces
{
    public interface IDepartmentService
    {
        void AddLecturesToDepartment(string departmentCode, IEnumerable<int> lectureIds);
        void AddStudentsToDepartment(string departmentCode, IEnumerable<int> studentNumbers);
        void CreateDepartment(Department department, IEnumerable<int> studentNumbers, IEnumerable<int> lectureIds);
        IEnumerable<Department> GetAllDepartments();
        IEnumerable<Lecture> GetLecturesByDepartment(string departmentCode);
        IEnumerable<Student> GetStudentsByDepartment(string departmentCode);
    }
}