﻿using Repository.Data.Entity;
using Repository.Models;
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
    public interface IUserServices
    {
        Task<ServiceResponse<UserResponse>> GetUser(string searchterm);
        Task<ServiceResponse<User>> UpdateUser(string userId, UserRequest user);
        Task<ServiceResponse<User>> DeleteUser(string userId);
    }
}
