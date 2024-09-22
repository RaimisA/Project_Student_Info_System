using Project_Student_Info_System.Database.Entities;

namespace Project_Student_Info_System.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        void Add(Department department);
        void Delete(string departmentCode);
        IEnumerable<Department> GetAll();
        Department GetById(string departmentCode);
        void Update(Department department);
    }
}