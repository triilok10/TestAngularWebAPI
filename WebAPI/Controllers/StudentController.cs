using Bussiness.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.CommonMaster;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        #region "Constructor"
        public readonly string _ConnectionString;
        public readonly IStudent _Student;
        public StudentController(IConfiguration configuration, IStudent student)
        {
            _Student = student;
            _ConnectionString = configuration.GetConnectionString("DBConnection");
        }
        #endregion


        #region "AddStudent"
        [HttpPost]
        public async Task<CommanMst> AddStudent(StudentRequest pStudent)
        {
            var result = await _Student.AddStudent(pStudent);
            return result;
        }
        #endregion


        [HttpGet]
        public async Task<CommanMst> GetAllStudents()
        {
            var result = await _Student.GetAllStudents();
            return result;
        }

        [HttpGet]
        public async Task<StateDistrictResponse> GetStateDistrictResponse(int? StateID)
        {
            var result = await _Student.StateDistrictResponse(StateID);
            return result;
        }

        [HttpGet]
        public async Task<CommanMst> GetStudentByID(int StudentID)
        {
            var result = await _Student.GetStudentByID(StudentID);
            return result;
        }


        [HttpPost]
        public async Task<CommanMst> EditStudent(StudentRequest pStudent)
        {
            var result = await _Student.EditStudent(pStudent);
            return result;
        }

        [HttpGet]
        public async Task<CommanMst> ClassMasterResponse()
        {
            var result = await _Student.ClassMasterResponse();
            return result;
        }

        [HttpDelete]
        public async Task<CommanMst> DeleteStudent(int StudentID)
        {
            var result = await _Student.DeleteStudent(StudentID);
            return result;
        }
    }
}
