using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Simple_Auth_System.DataAceessLayer;
using Simple_Auth_System.Models;

namespace Simple_Auth_System.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public readonly IConfiguration _configuration;

        public AuthController(IAuthDL authDL, IConfiguration configuration)
        {
            _authDL = authDL;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AuthCon").ToString());
                response = await _authDL.SignUp(request, connection);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AuthCon").ToString());
                response = await _authDL.SignIn(request, connection);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
