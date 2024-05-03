    using Microsoft.EntityFrameworkCore;
using StudentCRUD.Application;
using StudentCRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCRUD.Insfrastructure
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDBContext _dbContext;

        public StudentService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var result = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            var studentDetails = await _dbContext.Students.ToListAsync();
            return studentDetails;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var existingStudent = await _dbContext.Students.FindAsync(student.Id);

            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Phone = student.Phone;
            existingStudent.Gender = student.Gender;

            await _dbContext.SaveChangesAsync();

            return existingStudent;
        }

        //public async Task DeleteStudent(string id)
        //{
        //    var findStudent = await _dbContext.Students.FindAsync(id);

        //    _dbContext.Remove(findStudent);

        //    await _dbContext.SaveChangesAsync();
        //}



        public async Task DeleteStudent(string id)
        {
            var existingStudent = await _dbContext.Students.FindAsync(id);

            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }

            _dbContext.Remove(existingStudent);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentById(string id)
        {
            // Implement logic to retrieve student by ID from the database
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            return new List<Student> { student };
        }
    }
}
