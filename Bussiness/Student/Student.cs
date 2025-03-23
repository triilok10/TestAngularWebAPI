using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model.CommonMaster;

namespace Bussiness.Student
{
    public class Student : IStudent
    {

        #region "Constructor"
        public readonly string _ConnectionString;
        public Student(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("DBConnection");
        }
        #endregion

        #region "AddStudent"
        public async Task<CommanMst> AddStudent(StudentRequest pStudent)
        {
            var response = new CommanMst();
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_Student", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };


                    cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = "AddStudent";
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = pStudent.firstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = pStudent.lastName;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = pStudent.mobileNumber;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = pStudent.address;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = pStudent.gender;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = pStudent.email;

                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (rdr.HasRows)
                        {
                            while (await rdr.ReadAsync())
                            {
                                response.Status = Convert.ToInt32(rdr["Status"]);
                                response.Message = Convert.ToString(rdr["Message"]);
                            }
                        }
                        else
                        {
                            response.Status = 404;
                            response.Message = "No data returned.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 400;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
        #endregion

        public async Task<CommanMst> GetAllStudents()
        {
            var response = new CommanMst();
            List<StudentRequest> lst = new List<StudentRequest>();
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_Student", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GetAllStudents");
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var student = new StudentRequest
                            {
                                studentID = Convert.ToInt32(rdr["studentID"]),
                                firstName = Convert.ToString(rdr["firstName"]),
                                lastName = Convert.ToString(rdr["lastName"]),
                                mobileNumber = Convert.ToString(rdr["PhoneNumber"]),
                                address = Convert.ToString(rdr["address"]),
                                gender = Convert.ToString(rdr["gender"]),
                                email = Convert.ToString(rdr["email"]),
                            };
                            lst.Add(student);

                        }
                        response.data = lst;
                        response.Status = 200;
                        response.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<CommanMst> GetStudentByID(int StudentID)
        {
            var response = new CommanMst();
            StudentRequest obj = new StudentRequest();
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SP_Student", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Action", "GetStudentByID");
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj.studentID = Convert.ToInt32(rdr["StudentID"]);
                            obj.firstName = Convert.ToString(rdr["FirstName"]);
                            obj.lastName = Convert.ToString(rdr["LastName"]);
                            obj.mobileNumber = Convert.ToString(rdr["PhoneNumber"]);
                            obj.address = Convert.ToString(rdr["Address"]);
                            obj.gender = Convert.ToString(rdr["Gender"]);
                            obj.email = Convert.ToString(rdr["Email"]);
                            obj.state = rdr["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["StateID"]);
                            obj.district = rdr["DistrictID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DistrictID"]);
                            obj.studentClass = rdr["Class"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Class"]);
                        }
                    }
                    response.Status = 200;
                    response.Message = "Success";
                    response.data = obj;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<StateDistrictResponse> StateDistrictResponse(int? StateID)
        {
            StateDistrictResponse response = new StateDistrictResponse
            {
                lstState = new List<DropdownResponse>(),
                lstDistrict = new List<DropdownResponse>()
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    await con.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("SP_Student", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "StateMaster");

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                response.lstState.Add(new DropdownResponse
                                {
                                    Id = Convert.ToInt32(rdr["Id"]),
                                    Name = Convert.ToString(rdr["Name"])
                                });
                            }
                        }
                    }

                    if (StateID.HasValue && StateID > 0)
                    {
                        using (SqlCommand command = new SqlCommand("SP_Student", con))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Action", "DistrictMaster");
                            command.Parameters.AddWithValue("@StateID", StateID);

                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    response.lstDistrict.Add(new DropdownResponse
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = Convert.ToString(reader["Name"])
                                    });
                                }
                            }
                        }
                    }
                }

                response.Status = 200;
            }
            catch (Exception ex)
            {
                response.Status = 500;
            }

            return response;
        }


        public async Task<CommanMst> EditStudent(StudentRequest pStudent)
        {
            CommanMst mst = new CommanMst();
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_Student", con);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "EditStudent");
                    cmd.Parameters.AddWithValue("@StudentID", pStudent.studentID);
                    cmd.Parameters.AddWithValue("@FirstName", pStudent.firstName);
                    cmd.Parameters.AddWithValue("@LastName", pStudent.lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", pStudent.mobileNumber);
                    cmd.Parameters.AddWithValue("@Address", pStudent.address);
                    cmd.Parameters.AddWithValue("@Gender", pStudent.gender);
                    cmd.Parameters.AddWithValue("@Email", pStudent.email);
                    cmd.Parameters.AddWithValue("@StateID", pStudent.state);
                    cmd.Parameters.AddWithValue("@DistrictID", pStudent.district);
                    cmd.Parameters.AddWithValue("@StudentClass", pStudent.studentClass);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {

                            mst.Status = Convert.ToInt32(rdr["Status"]);
                            mst.Message = Convert.ToString(rdr["Message"]);
                        }
                        else
                        {
                            mst.Status = 404;
                            mst.Message = "No data returned.";
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return mst;
        }

        public async Task<CommanMst> ClassMasterResponse()
        {
            CommanMst mst = new CommanMst();
            List<DropdownResponse> classList = new List<DropdownResponse>();  

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_Student", con);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ClassMaster");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DropdownResponse classItem = new DropdownResponse
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                Name = Convert.ToString(rdr["Name"])
                            };

                            classList.Add(classItem);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            mst.Status = 200;
            mst.Message = string.Empty;
            mst.data = classList;

            return mst;
        }

    }
}
