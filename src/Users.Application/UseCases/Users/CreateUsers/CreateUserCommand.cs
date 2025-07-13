namespace Users.Application.UseCases.Users.CreateUsers
{
    public class CreateUserCommand(string email)
    {
        public string Email { get; set; } = email;
    }
}
