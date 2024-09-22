namespace Project_Student_Info_System.Database.Entities
{
    public class Lecture
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
