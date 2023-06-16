using Microsoft.Data.SqlClient;
using Simple_Auth_System.Models;
using System.Data;

namespace Simple_Auth_System.DataAceessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;

        //public readonly SqlConnection _connection;
        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            //_connection = new SqlConnection(_configuration.GetConnectionString("AuthCon").ToString());
            
        }
        public async Task<SignInResponse> SignIn(SignInRequest request,SqlConnection connection)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successfull!!";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from RegiForAuth where UserName='" + request.UserName + "' and Password='" + request.Password + "' and Role='" + request.Role + "'", connection);
                //SqlDataAdapter da = new SqlDataAdapter("Select * from RegiForAuth where UserName=@UserName and Password=@Password and Role=@Role",connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                if (dt.Rows.Count<= 0)
                {
                    response.IsSuccess =false;
                    response.Message = "Something Went wrong!!Login Failed";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Successfully registerd!!";

                }

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
               await connection.CloseAsync();
                await connection.DisposeAsync();
            }
            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request,SqlConnection connection)
        {
                
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "SuccessFull!";
            try
            {
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & confirm password should be same!!";
                    return response;
                }
                //SqlCommand cmd = new SqlCommand("Insert into Regi(UserName,Password,Role) values('" + request.UserName + "','" + request.Password + "','" + request.Role + "')", _connection);
                SqlCommand cmd = new SqlCommand("Insert into RegiForAuth(UserName,Password,Role) values(@UserName,@Password,@Role)", connection);
                cmd.Connection.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout= 180;
                cmd.Parameters.AddWithValue("@UserName", request.UserName);
                cmd.Parameters.AddWithValue("@Password", request.Password);
                cmd.Parameters.AddWithValue("@Role",request.Role);
                int status = await cmd.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;
                    response.Message= "Something went wrong!!";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Successfully registerd!!";
                }
                    

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                 await connection.CloseAsync();
                await connection.DisposeAsync();
            }
            return response;
        }
    }
}
