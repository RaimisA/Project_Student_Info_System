﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Database.Entities
{
    public class Department
    {
        public string DepartmentCode { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }

}
