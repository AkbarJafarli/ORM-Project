using ORM_Project.Enum;

namespace ORM_Project.Dtos.UserDtos;
public class OrderDto
{ 
    public int id {  get; set; }
    public int userId { get; set; }
    public DateTime orderDate { get; set; }
    public decimal totalAmount { get; set; }
    public OrderStatus status { get; set; }
}
