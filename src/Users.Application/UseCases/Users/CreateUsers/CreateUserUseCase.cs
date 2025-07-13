using Users.Application.Common;
using Users.Domain.Entities;
using Users.Infrastructure.Database;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly UsersDatabaseContext usersDatabaseContext;

        public CreateUserUseCase(UsersDatabaseContext usersDatabaseContext)
        {
            this.usersDatabaseContext = usersDatabaseContext ?? throw new ArgumentNullException(nameof(usersDatabaseContext));
        }
        public async Task<Result<int>> Handle(CreateUserCommand createUserCommand)
        {
            var user = new User()
                .WithEmail(createUserCommand.Email);

            await usersDatabaseContext.AddAsync(user);
            return Result.Success(user.Id);
        }
    }
}
