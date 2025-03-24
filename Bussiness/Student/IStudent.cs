using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.CommonMaster;

namespace Bussiness.Student
{
    public interface IStudent
    {
        Task<CommanMst> AddStudent(StudentRequest pStudent);
        Task<CommanMst> GetAllStudents();
        Task<CommanMst> GetStudentByID(int StudentID);
        Task<StateDistrictResponse> StateDistrictResponse(int? StateID);
        Task<CommanMst> ClassMasterResponse();
        Task<CommanMst> EditStudent(StudentRequest pStudent);
        Task<CommanMst> DeleteStudent(int StudentID);
    }
}
