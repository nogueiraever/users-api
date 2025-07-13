using Microsoft.EntityFrameworkCore;
using Users.Application.Common;
using Users.Infrastructure.Database;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public class UpdateUserEmailUseCase : IUpdateUserEmailUseCase
    {
        private readonly UsersDatabaseContext usersDatabaseContext;

        public UpdateUserEmailUseCase(UsersDatabaseContext usersDatabaseContext)
        {
            this.usersDatabaseContext = usersDatabaseContext ?? throw new ArgumentNullException(nameof(usersDatabaseContext));
        }
        public async Task<Result> Handle(UpdateUserEmailCommand updateUserEmailCommand)
        {
            var newUserEmail = updateUserEmailCommand.Email.ToLower();

            var user = await usersDatabaseContext
                .Users
                .FindAsync(updateUserEmailCommand.UserId);

            if (user == null)
                return Result.Failure("The user was not found");

            if (user.Email == newUserEmail)
                return Result.Success();

            var existingUserEmail = await usersDatabaseContext
                .Users
                .FirstOrDefaultAsync(c =>
                c.Email.ToLower() == newUserEmail
                && c.Id != updateUserEmailCommand.UserId);

            if (existingUserEmail != null)
                return Result.Failure("This email is already taken");

            user.WithEmail(newUserEmail);

            usersDatabaseContext.Update(user);
            await usersDatabaseContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
