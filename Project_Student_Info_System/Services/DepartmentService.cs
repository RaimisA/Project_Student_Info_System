using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Repositories.Interfaces;
using Project_Student_Info_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILectureRepository _lectureRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IStudentRepository studentRepository, ILectureRepository lectureRepository)
        {
            _departmentRepository = departmentRepository;
            _studentRepository = studentRepository;
            _lectureRepository = lectureRepository;
        }

        public void CreateDepartment(Department department, IEnumerable<int> studentNumbers, IEnumerable<int> lectureIds)
        {
            var students = _studentRepository.GetAll();
            var lectures = _lectureRepository.GetAll();

            department.Students = students.Where(s => studentNumbers.Contains(s.StudentNumber)).ToList();
            department.Lectures = lectures.Where(l => lectureIds.Contains(l.LectureId)).ToList();

            _departmentRepository.Add(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public void AddStudentsToDepartment(string departmentCode, IEnumerable<int> studentNumbers)
        {
            var department = _departmentRepository.GetById(departmentCode);
            var students = _studentRepository.GetAll();

            foreach (var studentNumber in studentNumbers)
            {
                var student = students.FirstOrDefault(s => s.StudentNumber == studentNumber);
                if (student != null)
                {
                    department.Students.Add(student);
                }
            }

            _departmentRepository.Update(department);
        }

        public void AddLecturesToDepartment(string departmentCode, IEnumerable<int> lectureIds)
        {
            var department = _departmentRepository.GetById(departmentCode);
            var lectures = _lectureRepository.GetAll();

            foreach (var lectureId in lectureIds)
            {
                var lecture = lectures.FirstOrDefault(l => l.LectureId == lectureId);
                if (lecture != null)
                {
                    department.Lectures.Add(lecture);
                }
            }

            _departmentRepository.Update(department);
        }

        public IEnumerable<Student> GetStudentsByDepartment(string departmentCode)
        {
            var department = _departmentRepository.GetById(departmentCode);
            return department.Students;
        }

        public IEnumerable<Lecture> GetLecturesByDepartment(string departmentCode)
        {
            var department = _departmentRepository.GetById(departmentCode);
            return department.Lectures;
        }
    }
}
