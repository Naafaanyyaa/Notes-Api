
using AutoMapper;
using FirstLab.Business.Exceptions;
using FirstLab.Business.Infrastructure;
using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using FirstLab.Data.Models;
using SavePets.Data.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace FirstLab.Business.Services;
public class UserService : IUserService 
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtHandler _jwtHandler;

    public UserService(IUserRepository userRepository, IMapper mapper, JwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var user = await _userRepository.GetByLogin(request.Name);

        if (user != null)
        {
            throw new NotFoundException($"User with such email: {request.Name} is already exists.");
        }

        user = _mapper.Map<RegisterRequest, User>(request);

        user.CreatedDate = DateTime.Now;

        var queryResult = await _userRepository.AddAsync(user);

        if (queryResult == null)
        {
            throw new Exception();
        }

        var result = _mapper.Map<User, RegisterResponse>(queryResult);

        return result;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByLogin(request.Name);

        if (user == null || !user.Password.Equals(request.Password))
        {
            return new LoginResponse()
            {
                ErrorMessage = "Invalid Authentication"
            };
        }
        var signingCredentials = _jwtHandler.GetSigningCredentials();
        var claims = await _jwtHandler.GetClaimsAsync(user);
        var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new LoginResponse() { IsAuthSuccessful = true, Token = token };
    }

    public async Task<List<UserResponse>> GetUserList()
    {
        var userList = await _userRepository.GetAllAsync();
        var result = _mapper.Map<List<User>, List<UserResponse>>(userList);

        return result;
    }

    public async Task<User> ChangePasswordAsync(ChangePasswordRequest request, string userId, string userName)
    {
        var userInfo = await _userRepository.GetByIdAsync(userId);

        if (!request.PreviousPassword.Equals(userInfo.Password))
        {
            throw new ValidationException("Password doesn't match.");
        }

        var user = _mapper.Map<ChangePasswordRequest, User>(request);

        user.Id = userId;
        user.Name = userName;
        user.LastModifiedDate = DateTime.Now;
        user.CreatedDate = userInfo.CreatedDate;

        await _userRepository.UpdateAsync(user);
 
        return user;
    }
}