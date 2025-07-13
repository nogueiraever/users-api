using Users.Application.Common;
using Users.Application.UseCases.Users.GetUserById;
using Users.Infrastructure.Database;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly UsersDatabaseContext usersDatabaseContext;

        public GetUserByIdUseCase(UsersDatabaseContext usersDatabaseContext)
        {
            this.usersDatabaseContext = usersDatabaseContext ?? throw new ArgumentNullException(nameof(usersDatabaseContext));
        }
        public async Task<Result<UserDto>> Handle(int userId)
        {
            var user = await usersDatabaseContext
                .Users
                .FindAsync(userId);

            if (user == null)
                return Result.Failure<UserDto>("The user was not found");


            return Result.Success(new UserDto().FromEntity(user));
        }
    }
}
