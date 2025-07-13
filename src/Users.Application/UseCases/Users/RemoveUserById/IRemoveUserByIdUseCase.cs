using Users.Application.Common;

namespace Users.Application.UseCases.Users.CreateUsers
{
    public interface IRemoveUserByIdUseCase
    {
        Task<Result> Handle(int userId);
    }
}