using System.Linq.Expressions;
using Architecture.Domain;
using Architecture.Domain.Interfaces;
using Architecture.Model;
using Architecture.Model.Tree;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database.Queries.QueriesCustome;


public interface ITreeQuery : IGetByIdQuery<Tree, TreeDto>
{

}

public class TreeQuery :GetByIdQuery<Tree, TreeDto> , ITreeQuery
{
    public TreeQuery(IQueryContext readContect) : base(readContect)
    {

    }

    public override async Task<TreeDto> QueryAsync(long id)
    {
        var query = this.Query<Tree>().Where(x => x.Id == id);

       // var r = query.ToList();

       var result = await query.Select(x => new TreeDto
       {
           Id = id,
           ParentId = x.ParentId,
           Description = new NameDto(x.Description.NameAr, x.Description.NameEn),
           FlateParent = x.FlateParent.Select(f => new TreeDto
           {
               Id = f.ParentId,
               ParentId = f.GrandParentId,
               Description = new NameDto(f.Parent.Description.NameAr, f.Parent.Description.NameEn)
           }).OrderBy(x => x.Id).ToList()

       }).FirstOrDefaultAsync();

       return result;
    }
}
