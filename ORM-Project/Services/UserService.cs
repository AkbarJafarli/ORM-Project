//using Microsoft.EntityFrameworkCore;
//using ORM_Project.Context;
//using ORM_Project.Dtos.UserDtos;
//using ORM_Project.Exceptions;
//using ORM_Project.Models;

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using ORM_Project.Context;
using ORM_Project.Dtos.UserDtos;
using ORM_Project.Exceptions;
using ORM_Project.Interfaces;
using ORM_Project.Models;

namespace ORM_Project.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService()
    {
        _context = new();
    }

    public async Task RegisterUserAsync(RegisterDto registerDto)
    {
        bool isCorrect = false;
        if (string.IsNullOrWhiteSpace(registerDto.email) || string.IsNullOrWhiteSpace(registerDto.password))
        {
            throw new InvalidUserInformationException("Email and password cannot be empty.");
        }
        var isExist = await _context.Users.AnyAsync(x => x.Email == registerDto.email);

        if (isExist)
            throw new InvalidUserInformationException("This email is already exist");

        var user = new User(registerDto.email, registerDto.password)
        {
            Fullname = registerDto.fullname,
            Address = registerDto.address,
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.email && u.Password == loginDto.password);
        if (user == null)
        {
            throw new UserAuthenticationException("Invalid email or password");
        }
        return user;
    }
    public async Task UpdateUserInfoAsync(UserDto userDto)
    {
        var user = await _context.Users.FindAsync(userDto.id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        user.Fullname = userDto.fullname;
        user.Email = userDto.email;
        user.Password = userDto.password;
        user.Address = userDto.address;

        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderDto>> GetUserOrderAsync(int userId)
    {
        var orders = await _context.Orders.Where(o => o.UserId == userId).Select(o => new OrderDto
        {
            id = o.Id,
            userId = o.UserId,
            orderDate = o.OrderDate,
            totalAmount = o.TotalAmount,
            status = o.Status,
        }).ToListAsync();
        return orders;
    }
    public async Task ExportUserOrdersToExcelAsync(int userId)
    {

    }









    //    private readonly AppDbContext _context;
    //    public UserService(AppDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public async Task RegisterUser(RegisterDto dto)
    //    {
    //        var isExist = await _context.Users.AnyAsync(x => x.Email == dto.email);
    //        //var isExist2 = await _context.Users.AnyAsync(x=>x.Fullname == dto.fullname);
    //        if (isExist)
    //        {
    //            throw new InvalidUserInformationException("Email or Fullname already used");
    //        }
    //        if (dto.email == null||dto.password==null)
    //        {
    //            throw new InvalidUserInformationException("User detailed cannot be empty");
    //        }
    //        User user = new(dto.email, dto.password);
    //        await _context.Users.AddAsync(user);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task LoginUser(LoginDto dto)
    //    {
    //        //var isCorrect = await _context.Users.AnyAsync(user=>user.Fullname == dto.fullname);
    //        //var isCorrect2 = await _context.Users.AnyAsync(user=>user.Password == dto.password);
    //        //if(isCorrect==false && isCorrect2 == false)
    //        //{
    //        //    throw new UserAuthenticationException("User detailed is not correct...");
    //        //}
    //        var user=await _context.Users.FirstOrDefaultAsync(x=>x.Email == dto.email);

    //        if(user == null)
    //            throw new UserAuthenticationException("User detailed is not correct...");

    //        if(user.Password!=dto.password)
    //            throw new UserAuthenticationException("User detailed is not correct...");

    //        Console.WriteLine($"Welcome {user.Fullname}");
    //    }
    //    public async Task UpdateUserInfo(UserDto dto)
    //    {
    //        var existUser = await _context.Users.FirstOrDefaultAsync( x=>x.Id == dto.id);
    //        if (existUser == null)
    //        {
    //            throw new NotFoundException("User not found");
    //        }
    //        existUser.Email = dto.email;
    //        existUser.Fullname = dto.fullname;
    //        existUser.Password = dto.password;
    //        existUser.Address = dto.address;
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task<IEnumerable<OrderDto>>GetUserOrderAsync(int userId)
    //    {
    //        var orders = await _context.Orders
    //            .Where(o=>o.UserId == userId)
    //            .Select(o=>new OrderDto
    //            {
    //                id = o.Id,
    //                userId = o.UserId,
    //                orderDate = o.OrderDate,
    //                totalAmount = o.TotalAmount,
    //                status= o.Status
    //            })
    //            .ToListAsync();
    //        if (!orders.Any())
    //        {
    //            throw new NotFoundException("No orders found for this user");
    //        }
    //        return orders;
    //    }


}

