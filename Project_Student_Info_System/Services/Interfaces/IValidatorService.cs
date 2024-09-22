using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Services.Interfaces
{
    public interface IValidatorService
    {
        void ValidateDepartment(Department department);
        void ValidateDepartmentCode(string code);
        void ValidateDepartmentExists(string departmentCode);
        void ValidateDepartmentName(string name);
        void ValidateEmail(string email);
        void ValidateLecture(Lecture lecture);
        void ValidateLectureDuration(DateTime startTime, DateTime endTime);
        void ValidateLectureExists(int lectureId);
        void ValidateLectureName(string name);
        void ValidateStudentName(string name);
        void ValidateStudent(Student student);
        void ValidateStudentExists(int studentNumber);
        void ValidateStudentNumber(int studentNumber);
        void ValidateUniqueDepartmentCode(string departmentCode);
        void ValidateUniqueLectureName(string lectureName);
        void ValidateUniqueStudentNumber(int studentNumber);
        string ValidateWeekDay(string weekDay);
        void ValidateLectureStartTime(DateTime startTime);
        void ValidateLectureEndTime(DateTime endTime);
        void ValidateLectureTime(DateTime startTime, DateTime endTime);
        void ValidateLectureOverlap(Lecture newLecture, IEnumerable<Lecture> existingLectures);
    }
}