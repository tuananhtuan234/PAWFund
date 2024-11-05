using Repository.Data.Entity;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthResponse>> Login(string email, string password);
        Task<ServiceResponse<User>> Register(UserRequest userRequest, string? code);
    }
}
