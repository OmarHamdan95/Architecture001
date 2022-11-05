// using System.Linq.Expressions;
// using Architecture.Database.Extension;
// using Architecture.Domain;
// using Architecture.Domain.Interfaces;
// using Architecture.Model;
// using DotNetCore.EntityFrameworkCore;
// using DotNetCore.Objects;
// using DotNetCore.Repositories;
// using Microsoft.EntityFrameworkCore;
//
// namespace Architecture.Database;
//
// public sealed class UserRepository : IAsyncRepository<User>
// {
//     // public UserRepository(Context context) : base(context) { }
//     //
//     // // public Task<long> GetAuthIdByUserIdAsync(long id)
//     // // {
//     // //     return Queryable.Where(UserExpression.Id(id)).Select(UserExpression.AuthId).SingleOrDefaultAsync();
//     // // }
//     // //
//     // // public Task<UserModel> GetModelAsync(long id)
//     // // {
//     // //     System.Diagnostics.Debugger.Launch();
//     // //     //var result = Queryable.WhereEven(x => x.Id);
//     // //
//     // //
//     // //     //var result2 = Queryable.StringStartWith(x => x.Name.NameAr, "Email");
//     // //
//     // //     //var t = Queryable.AsExtendable().WhereAny(x => x.Email == "e");
//     // //
//     // //     //var e = result2.Select(UserExpression.AuthId).SingleOrDefaultAsync();
//     // //     //var y = result.Select(UserExpression.AuthId).SingleOrDefaultAsync();
//     // //
//     // //     return Queryable.Where(UserExpression.Id(id)).Select(UserExpression.Model).SingleOrDefaultAsync();
//     // // }
//     // //
//     // // public Task<Grid<UserModel>> GridAsync(GridParameters parameters)
//     // // {
//     // //     return Queryable.Select(UserExpression.Model).GridAsync(parameters);
//     // // }
//     // //
//     // // public async Task<IEnumerable<UserModel>> ListModelAsync()
//     // // {
//     // //     return await Queryable.Select(UserExpression.Model).ToListAsync();
//     // // }
//     // //
//     // // public Task UpdateStatusAsync(User user)
//     // // {
//     // //     return UpdatePartialAsync(new { user.Id, user.Status });
//     // // }
//     //
//     // public Task<User> GetByIdAsync(long id)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task<User> GetByAsync(ISpecification<User> specification)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task<User> GetByAsync(Expression<Func<User, bool>> criteria)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task<List<User>> ListAllAsync()
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task<List<User>> ListAsync(ISpecification<User> specification)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task<User> AddAsync(User entity)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task UpdateAsync(User entity)
//     // {
//     //     throw new NotImplementedException();
//     // }
//     //
//     // public Task DeleteAsync(User entity)
//     // {
//     //     throw new NotImplementedException();
//     // }
// }
