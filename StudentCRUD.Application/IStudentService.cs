using StudentCRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCRUD.Application
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudent();
        Task<Student> UpdateStudent(Student student);
        Task DeleteStudent(string id);
        Task<IEnumerable<Student>> GetAllStudentById(string id);
    }
}
