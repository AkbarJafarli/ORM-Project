namespace ORM_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Product(int id, string name, decimal price, int stock, string description, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
        public Product()
        {

        }
    }


}
