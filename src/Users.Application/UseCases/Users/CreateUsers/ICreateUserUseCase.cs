using Users.Application.Common;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public interface ICreateUserUseCase
    {
        Task<Result<int>> Handle(CreateUserCommand createUserCommand);
    }
}