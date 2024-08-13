using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using ORM_Project.Context;
using ORM_Project.Exceptions;
using ORM_Project.Models;

namespace ORM_Project.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task MakePaymentAsync(Payment payment)
        {
            if (payment.Amount <= 0)
            {
                throw new InvalidPaymentException("Payment amount must be greater than zero.");
            }
            var order = await _context.Orders.FindAsync(payment.OrderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }
    }
}
