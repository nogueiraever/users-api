using Users.Domain.Entities;

namespace Users.Application.UseCases.Users.GetUserById
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public UserDto FromEntity(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Email = user.Email;
            }
            return this;
        }
    }
}
