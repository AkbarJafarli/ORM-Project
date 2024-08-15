namespace ORM_Project.Dtos.ProductDtos;
public class ProductCreateDto
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
    public string ProductDescription { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}