using Architecture.Model.Lookup;
using Architecture.Model.SystemMenu;

namespace Architecture.Database;

public interface ISystemMenuRepository : IRepository<SystemMenu>
{
    Task<SystemMenuModel> GetModelAsync(long id);

    Task<Grid<SystemMenuModel>> GridAsync(GridParameters parameters);

    Task<IEnumerable<SystemMenuModel>> ListModelAsync();
}
