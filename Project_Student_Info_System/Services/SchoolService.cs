using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly ILectureService _lectureService;

        public SchoolService(IStudentService studentService, IDepartmentService departmentService, ILectureService lectureService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _lectureService = lectureService;
        }
        
        //Servisų apjungimas
        // Student
        public void CreateStudent(Student student, string departmentCode, IEnumerable<int> lectureIds)
        {
            _studentService.CreateStudent(student, departmentCode, lectureIds);
        }

        public void TransferStudent(int studentNumber, string newDepartmentCode, IEnumerable<int> newLectureIds)
        {
            _studentService.TransferStudent(studentNumber, newDepartmentCode, newLectureIds);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentService.GetAllStudents();
        }

        // Department
        public void CreateDepartment(Department department, IEnumerable<int> studentNumbers, IEnumerable<int> lectureIds)
        {
            _departmentService.CreateDepartment(department, studentNumbers, lectureIds);
        }

        public void AddLecturesToDepartment(string departmentCode, IEnumerable<int> lectureIds)
        {
            _departmentService.AddLecturesToDepartment(departmentCode, lectureIds);
        }

        public void AddStudentsToDepartment(string departmentCode, IEnumerable<int> studentNumbers)
        {
            _departmentService.AddStudentsToDepartment(departmentCode, studentNumbers);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentService.GetAllDepartments();
        }

        public IEnumerable<Lecture> GetLecturesByDepartment(string departmentCode)
        {
            return _departmentService.GetLecturesByDepartment(departmentCode);
        }

        public IEnumerable<Student> GetStudentsByDepartment(string departmentCode)
        {
            return _departmentService.GetStudentsByDepartment(departmentCode);
        }

        // Lecture
        public void CreateLecture(Lecture lecture, string departmentCode)
        {
            _lectureService.CreateLecture(lecture, departmentCode);
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            return _lectureService.GetAllLectures();
        }

        public IEnumerable<Lecture> GetLecturesByStudent(int studentNumber)
        {
            return _lectureService.GetLecturesByStudent(studentNumber);
        }
    }
}
