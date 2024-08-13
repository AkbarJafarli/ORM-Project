using ORM_Project.Dtos.UserDtos;
using ORM_Project.Models;

namespace ORM_Project.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterDto registerDto);
        Task<User>LoginUserAsync(LoginDto loginDto);
        Task UpdateUserInfoAsync(UserDto userDto);
        Task<List<OrderDto>> GetUserOrderAsync(int userId);
        Task ExportUserOrdersToExcelAsync(int userId);
    }
}
