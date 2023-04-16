using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using AutoMapper;
using FirstLab.Business.Infrastructure;
using SavePets.Data.Interfaces;
using Amazon.Runtime.Internal;
using FirstLab.Business.Models.Response;

namespace FirstLab.Business.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticateService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<Claim>> Login(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByLogin(loginRequest.Name);

            if (user == null || !user.Password.Equals(loginRequest.Password))
            {
                throw new Exception("User doesn't exist");
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Name, user.Name)
            };

            return claims;
        }
    }
}
