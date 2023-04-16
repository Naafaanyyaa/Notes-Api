using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using System.Security.Claims;

namespace FirstLab.Business.Interfaces
{
    public interface IAuthenticateService
    {
        Task<List<Claim>> Login(LoginRequest loginRequest);
    }
}
