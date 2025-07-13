using Users.Application.Common;
using Users.Application.UseCases.Users.GetUserById;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public interface IGetUserByIdUseCase
    {
        Task<Result<UserDto>> Handle(int userId);
    }
}