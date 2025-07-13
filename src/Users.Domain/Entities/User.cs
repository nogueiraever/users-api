namespace Users.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public User WithEmail(string email)
        {
            Email = email;
            return this;
        }
    }
}
