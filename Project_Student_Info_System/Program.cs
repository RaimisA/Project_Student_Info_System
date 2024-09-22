using Project_Student_Info_System.Database;
using Project_Student_Info_System.Presentation;
using Project_Student_Info_System.Services.Interfaces;
using Project_Student_Info_System.Repositories.Interfaces;
using Project_Student_Info_System.Repositories;
using Project_Student_Info_System.Services;

namespace Project_Student_Info_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of SchoolContext
            SchoolContext context = new SchoolContext();

            // Create repository instances with the required context
            IStudentRepository studentRepository = new StudentRepository(context);
            IDepartmentRepository departmentRepository = new DepartmentRepository(context);
            ILectureRepository lectureRepository = new LectureRepository(context);

            // Create service instances with the required repositories
            IStudentService studentService = new StudentService(studentRepository, departmentRepository, lectureRepository);
            IDepartmentService departmentService = new DepartmentService(departmentRepository, studentRepository, lectureRepository);
            ILectureService lectureService = new LectureService(lectureRepository, departmentRepository, studentRepository);

            // Create the validator service
            IValidatorService validatorService = new ValidatorService(studentService, departmentService, lectureService);

            // Create the school service
            ISchoolService schoolService = new SchoolService(studentService, departmentService, lectureService);

            // Create the school presentation instance
            SchoolPresentation schoolPresentation = new SchoolPresentation(schoolService, validatorService);

            // Run the school presentation
            schoolPresentation.Run();
        }
    }
}
