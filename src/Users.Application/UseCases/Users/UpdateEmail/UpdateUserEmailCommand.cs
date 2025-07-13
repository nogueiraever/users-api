using System.Text.Json.Serialization;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public class UpdateUserEmailCommand(string email)
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Email { get; set; } = email;
    }
}
