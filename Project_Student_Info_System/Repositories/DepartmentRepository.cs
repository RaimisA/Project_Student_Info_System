using Project_Student_Info_System.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Student_Info_System.Database;
using Project_Student_Info_System.Repositories.Interfaces;

namespace Project_Student_Info_System.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchoolContext _context;

        public DepartmentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Department GetById(string departmentCode)
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Lectures)
                .FirstOrDefault(d => d.DepartmentCode == departmentCode);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Lectures)
                .ToList();
        }

        public void Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(string departmentCode)
        {
            var department = GetById(departmentCode);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }
    }
}
