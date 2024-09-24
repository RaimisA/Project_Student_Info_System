using Microsoft.EntityFrameworkCore;
using Project_Student_Info_System.Database;
using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Repositories;
using Project_Student_Info_System.Repositories.Interfaces;
using Project_Student_Info_System.Services;
using Project_Student_Info_System.Services.Interfaces;

namespace Project_Student_Tests
{
    [TestClass]
    public class SchoolTests
    {
        private SchoolContext _context;
        private DbContextOptions<SchoolContext> _dbOptions;
        private IStudentRepository _studentRepository;
        private IDepartmentRepository _departmentRepository;
        private ILectureRepository _lectureRepository;
        private IValidatorService _validatorService;
        private IStudentService _studentService;
        private IDepartmentService _departmentService;
        private ILectureService _lectureService;
        private ISchoolService _schoolService;
        private CsvHelper _csvHelper;

        [TestInitialize]
        public void OnInit()
        {
            _dbOptions = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "SchoolDatabase" + Guid.NewGuid())
                .Options;
            _context = new SchoolContext(_dbOptions);
            // Initialize repositories
            _studentRepository = new StudentRepository(_context);
            _departmentRepository = new DepartmentRepository(_context);
            _lectureRepository = new LectureRepository(_context);

            // Initialize services
            _studentService = new StudentService(_studentRepository, _departmentRepository, _lectureRepository);
            _departmentService = new DepartmentService(_departmentRepository, _studentRepository, _lectureRepository);
            _lectureService = new LectureService(_lectureRepository, _departmentRepository, _studentRepository);
            _validatorService = new ValidatorService(_studentService, _departmentService, _lectureService);
            _schoolService = new SchoolService(_studentService, _departmentService, _lectureService);
            _csvHelper = new CsvHelper();
            _context.Database.EnsureCreated();
        }

        private void SeedDatabase()
        {
            _context.ChangeTracker.Clear();

            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Seed Students
            var studentsPath = Path.Combine(basePath, "InitialData", "students.csv");
            var students = _csvHelper.GetStudents(studentsPath);
            foreach (var student in students)
            {
                var existingStudent = _context.Students.AsNoTracking().FirstOrDefault(s => s.StudentNumber == student.StudentNumber);
                if (existingStudent != null)
                {
                    _context.Entry(existingStudent).State = EntityState.Detached;
                }
                student.Lectures = new List<Lecture>(); // Initialize the Lectures collection
                _context.Students.Add(student);
            }

            // Seed Departments
            var departmentsPath = Path.Combine(basePath, "InitialData", "departments.csv");
            var departments = _csvHelper.GetDepartments(departmentsPath);
            foreach (var department in departments)
            {
                var existingDepartment = _context.Departments.AsNoTracking().FirstOrDefault(d => d.DepartmentCode == department.DepartmentCode);
                if (existingDepartment != null)
                {
                    _context.Entry(existingDepartment).State = EntityState.Detached;
                }
                department.Lectures = new List<Lecture>(); // Initialize the Lectures collection
                department.Students = new List<Student>(); // Initialize the Students collection
                _context.Departments.Add(department);
            }

            // Seed Lectures
            var lecturesPath = Path.Combine(basePath, "InitialData", "lectures.csv");
            var lectures = _csvHelper.GetLectures(lecturesPath);
            foreach (var lecture in lectures)
            {
                var existingLecture = _context.Lectures.AsNoTracking().FirstOrDefault(l => l.LectureId == lecture.LectureId);
                if (existingLecture != null)
                {
                    _context.Entry(existingLecture).State = EntityState.Detached;
                }
                lecture.Departments = new List<Department>(); // Initialize the Departments collection
                lecture.Students = new List<Student>(); // Initialize the Students collection
                _context.Lectures.Add(lecture);
            }

            _context.SaveChanges();

            // Seed DepartmentLectures
            var departmentLecturesPath = Path.Combine(basePath, "InitialData", "department_lectures.csv");
            var departmentLectures = _csvHelper.ReadCsv(departmentLecturesPath);
            foreach (var dl in departmentLectures)
            {
                var department = _context.Departments.First(d => d.DepartmentCode == dl[0]);
                var lecture = _context.Lectures.First(l => l.LectureId == int.Parse(dl[1]));
                if (!department.Lectures.Contains(lecture))
                {
                    department.Lectures.Add(lecture);
                }
            }

            // Seed StudentLectures
            var studentLecturesPath = Path.Combine(basePath, "InitialData", "student_lectures.csv");
            var studentLectures = _csvHelper.ReadCsv(studentLecturesPath);
            foreach (var sl in studentLectures)
            {
                var student = _context.Students.First(s => s.StudentNumber == int.Parse(sl[0]));
                var lecture = _context.Lectures.First(l => l.LectureId == int.Parse(sl[1]));
                if (!student.Lectures.Contains(lecture))
                {
                    student.Lectures.Add(lecture);
                }
            }

            _context.SaveChanges();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void ValidateLectureStartTime_ShouldNotThrowException_WhenTimeIsValid()
        {
            // Arrange
            DateTime validStartTime = new DateTime(2023, 1, 1, 10, 0, 0);

            // Act & Assert
            try
            {
                _validatorService.ValidateLectureStartTime(validStartTime);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }

        [TestMethod]
        public void ValidateStudentNameNull_ShouldThrowException_WhenNameIsInvalid()
        {
            var invalidNames = new[] { "", null };

            foreach (var name in invalidNames)
            {
                var student = new Student
                {
                    StudentNumber = 12345678,
                    FirstName = name,
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    DepartmentCode = "CS"
                };

                var exception = Assert.ThrowsException<ArgumentException>(() =>
                    _validatorService.ValidateStudent(student));
                Assert.AreEqual("Name must be at least 2 characters long.", exception.Message);
            }
        }

        [TestMethod]
        public void ValidateStudentName_ShouldThrowException_WhenNameIsInvalid()
        {
            // Arrange
            string invalidName = "Jo1n";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentName(invalidName));
            Assert.AreEqual("Name must contain only letters.", exception.Message);
        }

        // Add more tests for other validation scenarios

        [TestMethod]
        public void ValidateStudentName_ShouldThrowException_WhenNameContainsNonLetters()
        {
            // Arrange
            string invalidName = "Jo1n";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentName(invalidName));
            Assert.AreEqual("Name must contain only letters.", exception.Message);
        }

        [TestMethod]
        public void ValidateStudentName_ShouldThrowException_WhenNameIsTooShort()
        {
            // Arrange
            string invalidName = "J";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentName(invalidName));
            Assert.AreEqual("Name must be at least 2 characters long.", exception.Message);
        }

        [TestMethod]
        public void ValidateStudentName_ShouldThrowException_WhenNameIsTooLong()
        {
            // Arrange
            string invalidName = "JohnathonJohnathonJohnathonJohnathonJohnathonJohnathon";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentName(invalidName));
            Assert.AreEqual("Name must be no more than 50 characters long.", exception.Message);
        }

        [TestMethod]
        public void ValidateStudentNameShouldThrowException_WhenSurnameContainsNonLetters()
        {
            // Arrange
            string invalidName = "Sm!th";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentName(invalidName));
            Assert.AreEqual("Name must contain only letters.", exception.Message);
        }

        [TestMethod]
        public void ValidateStudentNumber_ShouldThrowException_WhenNumberIsTooShort()
        {
            // Arrange
            int invalidNumber = 1234567;

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentNumber(invalidNumber));
            Assert.AreEqual("Student number must be exactly 8 digits.", exception.Message);
        }

        [TestMethod]
        public void ValidateStudentNumber_ShouldThrowException_WhenNumberIsTooLong()
        {
            // Arrange
            int invalidNumber = 123456789;

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentNumber(invalidNumber));
            Assert.AreEqual("Student number must be exactly 8 digits.", exception.Message);
        }


        [TestMethod]
        public void ValidateStudentNumber_ShouldThrowException_WhenNumberIsNotEightDigits()
        {
            // Arrange
            var validatorService = new ValidatorService(_studentService, _departmentService, _lectureService);
            var invalidStudentNumber = 1234; // Example of an invalid student number with less than 8 digits

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => validatorService.ValidateStudentNumber(invalidStudentNumber));
            Assert.AreEqual("Student number must be exactly 8 digits.", ex.Message);
        }


        [TestMethod]
        public void ValidateStudentNumber_ShouldThrowException_WhenNumberIsNotUnique()
        {
            // Arrange
            var student1 = new Student
            {
                StudentNumber = 12345678,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DepartmentCode = "CS0050"
            };

            var student2 = new Student
            {
                StudentNumber = 12345678, // Duplicate student number
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                DepartmentCode = "CS0050"
            };

            _studentService.CreateStudent(student1, student1.DepartmentCode, new List<int>());

            // Detach the first student to avoid tracking conflict
            _context.Entry(student1).State = EntityState.Detached;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
                _validatorService.ValidateUniqueStudentNumber(student2.StudentNumber),
                "Student number must be unique.");
        }

        [TestMethod]
        public void ValidateStudentNumber_ShouldThrowMultipleExceptions_WhenNumberIsInvalid()
        {
            // Arrange
            var invalidNumbers = new List<int>
                {
                    1234,       // Too short
                    123456789,  // Too long
                    -12345678   // Negative number
                };

            foreach (var invalidNumber in invalidNumbers)
            {
                // Act & Assert
                var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateStudentNumber(invalidNumber));
                Assert.AreEqual("Student number must be exactly 8 digits.", exception.Message);
            }
        }

        [TestMethod]
        public void ValidateDepartmentName_ShouldThrowException_WhenNameIsTooShort()
        {
            // Arrange
            string invalidName = "CS";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateDepartmentName(invalidName));
            Assert.AreEqual("Department name must be between 3 and 100 characters and contain only letters and numbers.", exception.Message);
        }

        [TestMethod]
        public void ValidateDepartmentName_ShouldThrowException_WhenNameContainsSpecialCharacters()
        {
            // Arrange
            string invalidName = "Computer Science & Engineering";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateDepartmentName(invalidName));
            Assert.AreEqual("Department name must be between 3 and 100 characters and contain only letters and numbers.", exception.Message);
        }

        [TestMethod]
        public void ValidateDepartmentCode_ShouldThrowException_WhenCodeIsTooShort()
        {
            // Arrange
            string invalidCode = "CS12";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateDepartmentCode(invalidCode));
            Assert.AreEqual("Department code must be exactly 6 characters and contain only letters and numbers.", exception.Message);
        }

        [TestMethod]
        public void ValidateDepartmentCode_ShouldThrowException_WhenCodeContainsSpecialCharacters()
        {
            // Arrange
            string invalidCode = "CS123@";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateDepartmentCode(invalidCode));
            Assert.AreEqual("Department code must be exactly 6 characters and contain only letters and numbers.", exception.Message);
        }

        //[TestMethod]
        //public void ValidateStudentDepartment_ShouldThrowException_WhenDepartmentIsMissing()
        //{
        //    var student = new Student
        //    {
        //        StudentNumber = 12345678,
        //        FirstName = "John",
        //        LastName = "Smith",
        //        Email = "john.smith@example.com",
        //        DepartmentCode = null // Missing department
        //    };

        //    var exception = Assert.ThrowsException<ArgumentException>(() =>
        //        _validatorService.ValidateStudent(student));
        //    Assert.AreEqual("Department is required.", exception.Message);
        //}

        //[TestMethod]
        //public void ValidateDepartmentCode_ShouldThrowException_WhenCodeIsNotUnique()
        //{
        //    // Arrange
        //    var department1 = new Department
        //    {
        //        DepartmentCode = "CS0001",
        //        Name = "Computer Science"
        //    };

        //    var department2 = new Department
        //    {
        //        DepartmentCode = "CS0001", // Duplicate code
        //        Name = "Cyber Security"
        //    };

        //    _departmentService.CreateDepartment(department1, new List<int>(), new List<int>());

        //    // Act & Assert
        //    var exception = Assert.ThrowsException<ArgumentException>(() =>
        //        _validatorService.ValidateDepartmentCode(department2.DepartmentCode));
        //    Assert.AreEqual("Department code must be unique.", exception.Message);
        //}

        [TestMethod]
        public void ValidateEmail_ShouldThrowException_WhenEmailIsInvalid()
        {
            // Arrange
            var invalidEmails = new List<string>
    {
        "john.smithexample.com", // Missing @
        "john.smith@", // Missing domain
        "@example.com", // Missing local part
        "john.smith@example", // Missing domain suffix
        "john.smith@example." // Missing domain suffix
    };

            foreach (var email in invalidEmails)
            {
                // Act & Assert
                var exception = Assert.ThrowsException<ArgumentException>(() =>
                    _validatorService.ValidateEmail(email));
                Assert.AreEqual("Email must be in a valid format.", exception.Message);
            }
        }

        [TestMethod]
        public void AddLecture_ShouldSucceed_WhenLectureIsAssignedToDifferentDepartment()
        {
            // Arrange
            var newLecture = new Lecture
            {
                Name = "Physics",
                StartTime = new DateTime(2023, 1, 1, 13, 0, 0),
                EndTime = new DateTime(2023, 1, 1, 14, 30, 0),
                WeekDay = "Wednesday"
            };

            var departmentCode = "PHYS101";

            var department = new Department
            {
                DepartmentCode = departmentCode,
                Name = "Physics"
            };

            _context.Departments.Add(department);
            _context.SaveChanges();

            // Act
            _schoolService.CreateLecture(newLecture, departmentCode);

            // Assert
            var lectures = _context.Lectures.Where(l => l.Departments.Any(d => d.DepartmentCode == departmentCode)).ToList();
            Assert.IsTrue(lectures.Any(l => l.Name == "Physics"));
        }

        [TestMethod]
        public void ValidateLectureExists_ShouldThrowException_WhenLectureNotFound()
        {
            // Arrange
            var nonExistentLectureId = 999; // Assuming this ID does not exist

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateLectureExists(nonExistentLectureId));
            Assert.AreEqual("Lecture not found.", ex.Message);
        }

        [TestMethod]
        public void ValidateLectureExists_ShouldNotThrowException_WhenLectureExists()
        {
            // Arrange
            var uniqueLectureId = 1;

            // Ensure the database is clean
            _context.Lectures.RemoveRange(_context.Lectures);
            _context.SaveChanges();

            // Seed the database with a lecture
            var lecture = new Lecture
            {
                LectureId = uniqueLectureId,
                Name = "Introduction to Programming",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                WeekDay = "Monday"
            };
            _context.Lectures.Add(lecture);
            _context.SaveChanges();

            // Act & Assert
            _validatorService.ValidateLectureExists(uniqueLectureId);
        }

        [TestMethod]
        public void ValidateLectureName_ShouldThrowException_WhenNameIsTooShort()
        {
            // Arrange
            string invalidName = "Math";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateLectureName(invalidName));
            Assert.AreEqual("Lecture name must be at least 5 characters long.", exception.Message);
        }

        [TestMethod]
        public void ValidateLectureTime_ShouldThrowException_WhenStartTimeIsOutOfRange()
        {
            // Arrange
            var startTime = new DateTime(1, 1, 1, 23, 30, 0);
            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateLectureStartTime(startTime));
            Assert.AreEqual("Lecture time must be between 00:00 and 23:00.", ex.Message);
        }

        [TestMethod]
        public void ValidateLectureTime_ShouldThrowException_WhenEndTimeIsBeforeStartTime()
        {
            // Arrange
            var startTime = new DateTime(1, 1, 1, 14, 0, 0);
            var endTime = new DateTime(1, 1, 1, 13, 00, 0);

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateLectureTime(startTime, endTime));
            Assert.AreEqual("Lecture start time must be before lecture end time.", exception.Message);
        }

        //[TestMethod]
        //public void ValidateLectureTime_ShouldThrowException_WhenTimesOverlap()
        //{
        //    // Arrange
        //    var lecture1 = new Lecture
        //    {
        //        LectureId = 1,
        //        Name = "Math 101",
        //        WeekDay = "Monday",
        //        StartTime = new DateTime(2023, 1, 1, 9, 0, 0),
        //        EndTime = new DateTime(2023, 1, 1, 10, 0, 0)
        //    };

        //    var lecture2 = new Lecture
        //    {
        //        LectureId = 2, // Ensure unique LectureId
        //        Name = "Physics 101",
        //        WeekDay = "Monday",
        //        StartTime = new DateTime(1, 1, 1, 9, 30, 0), // Overlapping time
        //        EndTime = new DateTime(1, 1, 1, 10, 30, 0)
        //    };

        //    _lectureService.CreateLecture(lecture1, "CS0050");

        //    // Detach the first lecture to avoid tracking conflict
        //    _context.Entry(lecture1).State = EntityState.Detached;

        //    _lectureService.CreateLecture(lecture2, "CS0050");

        //    // Act & Assert
        //    var exception = Assert.ThrowsException<ArgumentException>(() =>
        //        _validatorService.ValidateLectureOverlap(lecture2, new List<Lecture> { lecture1 }));
        //    Assert.AreEqual("Lecture times overlap.", exception.Message);
        //}

        [TestMethod]
        public void ValidateLectureDay_ShouldThrowException_WhenDayIsInvalid()
        {
            // Arrange
            string invalidDay = "Sunday";

            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => _validatorService.ValidateWeekDay(invalidDay));
            Assert.AreEqual("Invalid day of the week. Valid days are Monday through Friday.", exception.Message);
        }

        [TestMethod]
        public void ValidateLectureNameNull_ShouldThrowException_WhenNameIsInvalid()
        {
            var invalidNames = new[] { "", null };

            foreach (var name in invalidNames)
            {
                var exception = Assert.ThrowsException<ArgumentException>(() =>
                    _validatorService.ValidateLectureName(name));
                Assert.AreEqual("Lecture name must be at least 5 characters long.", exception.Message);
            }
        }

        [TestMethod]
        public void ValidateLectureDay_ShouldDefaultToWeekdays_WhenDayIsNull()
        {
            string nullDay = null;

            nullDay = _validatorService.ValidateWeekDay(nullDay);
            var validWeekdays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            Assert.IsTrue(validWeekdays.Contains(nullDay));
        }

        [TestMethod]
        public void TransferStudent_ShouldSucceed_WhenDepartmentExists()
        {
            // Arrange
            var existingDepartment = new Department
            {
                DepartmentCode = "CS",
                Name = "Computer Science"
            };

            var newDepartment = new Department
            {
                DepartmentCode = "EE",
                Name = "Electrical Engineering"
            };

            var student = new Student
            {
                StudentNumber = 123,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com", // Added Email property
                DepartmentCode = existingDepartment.DepartmentCode
            };

            _context.Departments.Add(existingDepartment);
            _context.Departments.Add(newDepartment);
            _context.Students.Add(student);
            _context.SaveChanges();

            var newLectureIds = new List<int> { 1, 2 };

            // Act
            _studentService.TransferStudent(student.StudentNumber, newDepartment.DepartmentCode, newLectureIds);

            // Assert
            var updatedStudent = _studentRepository.GetById(student.StudentNumber);
            Assert.AreEqual(newDepartment.DepartmentCode, updatedStudent.DepartmentCode);
        }
    }
}