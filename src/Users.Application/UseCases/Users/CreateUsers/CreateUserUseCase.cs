using Microsoft.EntityFrameworkCore;
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
            var newUserEmail = createUserCommand.Email.ToLower();

            var user = await usersDatabaseContext
                .Users
                .FirstOrDefaultAsync(c => c.Email.ToLower() == newUserEmail);
            
            if (user != null)
                return Result.Failure<int>("This email is already taken");

            user = new User()
            .WithEmail(newUserEmail);

            await usersDatabaseContext.AddAsync(user);
            await usersDatabaseContext.SaveChangesAsync();
            return Result.Success(user.Id);
        }
    }
}
