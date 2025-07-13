using Microsoft.EntityFrameworkCore;
using Users.Application.UseCases.Users.CreateUsers;
using Users.Domain.Entities;
using Users.Infrastructure.Database;

namespace Users.Tests.Application
{
    public class CreateUserUseCaseTest : IDisposable
    {
        private readonly UsersDatabaseContext _context;
        private readonly CreateUserUseCase _useCase;

        public CreateUserUseCaseTest()
        {
            var options = new DbContextOptionsBuilder<UsersDatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new UsersDatabaseContext(options);
            _useCase = new CreateUserUseCase(_context);
        }

        [Fact]
        public async Task ShouldCreateUserWhenNewEmail()
        {
            var command = new CreateUserCommand("newuser@example.com");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value > 0);

            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "newuser@example.com");
            Assert.NotNull(createdUser);
            Assert.Equal("newuser@example.com", createdUser.Email);
            Assert.Equal(result.Value, createdUser.Id);
        }

        [Fact]
        public async Task ShouldReturnFailureWhenEmailAlreadyTaken()
        {
            var existingUser = new User().WithEmail("existing@example.com");
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var command = new CreateUserCommand("existing@example.com");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsFailure);
            Assert.Equal("This email is already taken", result.Error);
            Assert.Equal(0, result.Value);

            var userCount = await _context.Users.CountAsync(u => u.Email == "existing@example.com");
            Assert.Equal(1, userCount);
        }

        [Fact]
        public async Task ShouldReturnFailureWhenUsingUpperCaseButExistingEmail()
        {
            var existingUser = new User().WithEmail("existing@example.com");
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var command = new CreateUserCommand("EXISTING@EXAMPLE.COM");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsFailure);
            Assert.Equal("This email is already taken", result.Error);
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public async Task ShouldAddNewUserAsLowerCaseEmail()
        {
            var command = new CreateUserCommand("NewUser@Example.COM");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value > 0);

            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == result.Value);
            Assert.NotNull(createdUser);
            Assert.Equal("newuser@example.com", createdUser.Email);
        }

        [Fact]
        public async Task ShouldCreateNewUserSuccessfully()
        {
            var existingUsers = new[]
            {
                new User().WithEmail("user1@example.com"),
                new User().WithEmail("user2@example.com"),
                new User().WithEmail("user3@example.com")
            };

            await _context.Users.AddRangeAsync(existingUsers);
            await _context.SaveChangesAsync();

            var command = new CreateUserCommand("newuser@example.com");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value > 0);

            var totalUsers = await _context.Users.CountAsync();
            Assert.Equal(4, totalUsers);

            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "newuser@example.com");
            Assert.NotNull(createdUser);
        }

        [Fact]
        public async Task ShouldReturnCorrectUserIdWhenAddingNewUser()
        {
            var command = new CreateUserCommand("test@example.com");

            var result = await _useCase.Handle(command);

            Assert.True(result.IsSuccess);
            
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.NotNull(savedUser);
            Assert.Equal(result.Value, savedUser.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
