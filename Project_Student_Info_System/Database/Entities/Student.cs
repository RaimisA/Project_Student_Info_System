namespace Project_Student_Info_System.Database.Entities
{
    public class Student 
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DepartmentCode { get; set; } //FK
        public Department Department { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
