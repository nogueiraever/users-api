using Users.Application.Common;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public interface IUpdateUserEmailUseCase
    {
        Task<Result> Handle(UpdateUserEmailCommand updateUserEmailCommand);
    }
}