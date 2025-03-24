using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonMaster
{
    public class CommanMst
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic data { get; set; }
    }

    public class StudentRequest
    {
        public int studentID { get; set; }
        public string studentPhoto { get; set; }
        public string fileExtension { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public int state { get; set; }
        public int district { get; set; }
        public int studentClass { get; set; }
    }

    public class MstResponse
    {
        public int Status { get; set; }
        public int Message { get; set; }
    }

    public class DropdownResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StateDistrictResponse
    {
        public int Status { get; set; }
        public List<DropdownResponse> lstState { get; set; }
        public List<DropdownResponse> lstDistrict { get; set; }
    }
}
