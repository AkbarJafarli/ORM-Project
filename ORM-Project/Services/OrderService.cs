using Microsoft.EntityFrameworkCore;
using ORM_Project.Context;
using ORM_Project.Enum;
using ORM_Project.Exceptions;
using ORM_Project.Models;

namespace ORM_Project.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            if (order.TotalAmount < 0)
            {
                throw new InvalidOrderException("Order amount connot be negative");
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == order.UserId);
            if (!userExists)
            {
                throw new NotFoundException("User not found");
            }
            order.Status = OrderStatus.Pending;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Status == OrderStatus.Cancelled)
            {
                throw new OrderAlreadyCancelledException("Order has already been cancelled.");
            }
            order.Status = OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
        public async Task CompleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (order.Status == OrderStatus.Completed)
            {
                throw new InvalidOrderException("Order is already completed");
            }
            order.Status = OrderStatus.Completed;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrderAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            if (orderDetail.Quantity < 0)
            {
                throw new InvalidOrderDetailException("Quantity cannot be negative");
            }
            if (orderDetail.PricePerItem < 0)
            {
                throw new InvalidOrderDetailException("Price per item cannot be negative");
            }
            var order = await _context.Orders.FindAsync(orderDetail.OrderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            var product = await _context.Products.FindAsync(orderDetail.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>>GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(od=>od.OrderId == orderId).ToListAsync();
            if (!orderDetails.Any())
            {
                throw new NotFoundException("Order details not found for the given order ID.");
            }
            return orderDetails;
        }
    }
}
