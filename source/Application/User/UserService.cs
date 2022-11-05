using Architecture.Database;
using Architecture.Database.Queries;
using Architecture.Database.UnitOfWork;
using Architecture.Domain;
using Architecture.Domain.Interfaces;
using Architecture.Model;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using DotNetCore.Results;
using DotNetCore.Validation;
using Mapster;

namespace Architecture.Application;

public sealed class UserService : IUserService
{
    private readonly IAuthService _authService;
    private readonly IAsyncUnitOfWork _unitOfWork;
    private readonly IUserFactory _userFactory;
    private readonly IAsyncRepository<User> _userRepository;
    private readonly IGetByIdQuery<User, UserModel> _userGetByIdQuery;
    private readonly IListQuery<User, UserModel> _userListQuery;

    public UserService
    (
        IAuthService authService,
        IAsyncUnitOfWork unitOfWork,
        IUserFactory userFactory,
        IAsyncRepository<User> userRepository,
        IGetByIdQuery<User, UserModel> userGetByIdQuery,
        IListQuery<User, UserModel> userListQuery
    )
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
        _userFactory = userFactory;
        _userRepository = userRepository;
        _userGetByIdQuery = userGetByIdQuery;
        _userListQuery = userListQuery;
    }

    public async Task<IResult<long>> AddAsync(UserModel model)
    {
        var validation = new AddUserModelValidator().Validation(model);

        if (validation.Failed) return validation.Fail<long>();

        var auth = await _authService.AddAsync(model.Auth);

        if (auth.Failed) return auth.Fail<long>();

        var user = _userFactory.Create(model, auth.Data);

        await _userRepository.AddAsync(user);

        await _unitOfWork.EndAsync();

        return user.Id.Success();
    }

    public async Task<IResult> DeleteAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        await _userRepository.DeleteAsync(user);

        await _authService.DeleteAsync(user.Auth.Id);

        await _unitOfWork.EndAsync();

        return Result.Success();
    }

    public Task<UserModel> GetAsync(long id)
    {
        return _userGetByIdQuery.QueryAsync(id);
    }

    public Task<Grid<UserModel>> GridAsync(GridParameters parameters)
    {
        return null;
    }

    public async Task<IResult> InactivateAsync(long id)
    {
        var user = new User(id);

        user.Inactivate();

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.EndAsync();

        return Result.Success();
    }

    public async Task<List<UserModel>> ListAsync()
    {
        var res = (await _userListQuery.QueryAsync()).Adapt<List<UserModel>>();
        return  res;
    }

    public async Task<IResult> UpdateAsync(UserModel model)
    {
        var validation = new UpdateUserModelValidator().Validation(model);

        if (validation.Failed) return validation;

        var user = await _userRepository.GetByIdAsync(model.Id);

        if (user is null) return Result.Success();

        user.Update(model.FirstName, model.LastName, model.Email);

        await _userRepository.UpdateAsync(user);

        await _unitOfWork.EndAsync();

        return Result.Success();
    }
}
