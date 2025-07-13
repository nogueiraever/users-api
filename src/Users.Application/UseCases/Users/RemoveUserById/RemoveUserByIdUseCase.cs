using Users.Application.Common;
using Users.Infrastructure.Database;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public class RemoveUserByIdUseCase : IRemoveUserByIdUseCase
    {
        private readonly UsersDatabaseContext usersDatabaseContext;

        public RemoveUserByIdUseCase(UsersDatabaseContext usersDatabaseContext)
        {
            this.usersDatabaseContext = usersDatabaseContext ?? throw new ArgumentNullException(nameof(usersDatabaseContext));
        }
        public async Task<Result> Handle(int userId)
        {
            var user = await usersDatabaseContext
                .Users
                .FindAsync(userId);

            if (user == null)
                return Result.Failure("The user was not found");

            usersDatabaseContext.Remove(user);

            await usersDatabaseContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
