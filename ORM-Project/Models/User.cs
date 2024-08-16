namespace ORM_Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }


        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
