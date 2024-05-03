using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentCRUD.Application;
using StudentCRUD.Domain;

namespace StudentCRUD.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }
        
        private string BuildJWTToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hdcgbjhsbdcjhdbshvcdjfbvuhdbhuvbjsbvjbdevfubdvbdvbjndbvbdsfbchdusbcusdbcjhds"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = "https://localhost:44341";
            var audience = "https://localhost:44341";
            var jwtValidity = DateTime.Now.AddDays(15);



            var token = new JwtSecurityToken(
         issuer,
         audience,
         claims: new[] { new Claim(ClaimTypes.Email, email) }, // Add email claim
         expires: jwtValidity,
         signingCredentials: creds
     );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("generate")]
        public IActionResult GenerateToken(string email)
        {
            var jwtToken = BuildJWTToken(email);
            return Ok(new { Token = jwtToken });
        }


        [HttpPost, Route("create-student")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                var addedStudent = await _student.AddStudent(student);
                return Ok(addedStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add student");
            }
        }

        [HttpPost, Route("update-student")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            try
            {
                var updatedStudent = await _student.UpdateStudent(student);
                return Ok(updatedStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update student");
            }
        }

        [HttpDelete, Route("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                await _student.DeleteStudent(id);
                return Ok("Student deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete student");
            }
        }

        [HttpGet, Route("get-student")]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                var students = await _student.GetAllStudent();
                return Ok(students);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve students");
            }
        }
    }
}