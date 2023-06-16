using Microsoft.Data.SqlClient;
using Simple_Auth_System.Models;

namespace Simple_Auth_System.DataAceessLayer
{
    public interface IAuthDL
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request,SqlConnection connection);
        public Task<SignInResponse> SignIn(SignInRequest request,SqlConnection connection);
    }
}
