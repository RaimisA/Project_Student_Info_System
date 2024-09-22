using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Repositories.Interfaces;
using Project_Student_Info_System.Services.Interfaces;

namespace Project_Student_Info_System.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILectureRepository _lectureRepository;

        public StudentService(IStudentRepository studentRepository, IDepartmentRepository departmentRepository, ILectureRepository lectureRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _lectureRepository = lectureRepository;
        }

        public void CreateStudent(Student student, string departmentCode, IEnumerable<int> lectureIds)
        {
            var department = _departmentRepository.GetById(departmentCode);
            var lectures = _lectureRepository.GetAll();

            student.Department = department;
            student.Lectures = lectures.Where(l => lectureIds.Contains(l.LectureId)).ToList();

            _studentRepository.Add(student);
        }

        public void TransferStudent(int studentNumber, string newDepartmentCode, IEnumerable<int> newLectureIds)
        {
            var student = _studentRepository.GetById(studentNumber);
            var newDepartment = _departmentRepository.GetById(newDepartmentCode);
            var newLectures = _lectureRepository.GetAll();

            student.Department = newDepartment;
            student.Lectures = newLectures.Where(l => newLectureIds.Contains(l.LectureId)).ToList();

            _studentRepository.Update(student);
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }
    }
}
