using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Repositories.Interfaces;
using Project_Student_Info_System.Services.Interfaces;

namespace Project_Student_Info_System.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStudentRepository _studentRepository;

        public LectureService(ILectureRepository lectureRepository, IDepartmentRepository departmentRepository, IStudentRepository studentRepository)
        {
            _lectureRepository = lectureRepository;
            _departmentRepository = departmentRepository;
            _studentRepository = studentRepository;
        }

        public void CreateLecture(Lecture lecture, string departmentCode)
        {
            var department = _departmentRepository.GetById(departmentCode);
            department.Lectures.Add(lecture);

            _lectureRepository.Add(lecture);
            _departmentRepository.Update(department);
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            return _lectureRepository.GetAll();
        }

        public IEnumerable<Lecture> GetLecturesByStudent(int studentNumber)
        {
            var student = _studentRepository.GetById(studentNumber);
            if (student == null)
            {
                throw new ArgumentException("Student not found.");
            }

            // Ensure that the Lectures property is not null
            return student.Lectures ?? Enumerable.Empty<Lecture>();
        }
    }
}
