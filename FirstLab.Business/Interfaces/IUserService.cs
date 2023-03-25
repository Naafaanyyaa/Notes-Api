
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using FirstLab.Data.Models;

namespace FirstLab.Business.Interfaces;

public interface IUserService
{
    public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    public Task<LoginResponse> LoginAsync(LoginRequest request);
    public Task<List<UserResponse>> GetUserList();
    public Task<User> ChangePasswordAsync(ChangePasswordRequest request, string userId, string userName);
}