using System.Linq.Expressions;
using DotNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Architecture.Database.Queries;
using Architecture.Database.Queries.QueriesCustome;
using Architecture.Domain;
using Architecture.Model;
using Architecture.Model.Tree;
using DotNetCore.AspNetCore;

namespace Architecture.Web;

[AllowAnonymous]
[ApiController]
[Route("diagnostics")]
public sealed class DiagnosticController : ControllerBase
{
    private IGetByIdQuery<Tree, TreeDto> _getByIdQuery;
    private ITreeQuery _getTreeBYIdQuery;

    public DiagnosticController(IGetByIdQuery<Tree, TreeDto> getByIdQuery,
        ITreeQuery getTreeBYIdQuery)
    {
        _getByIdQuery = getByIdQuery;
        _getTreeBYIdQuery = getTreeBYIdQuery;
    }

    [HttpGet("datetime")]
    public DateTime DateTime() => Assembly.GetExecutingAssembly().FileInfo().LastWriteTime;

    [HttpGet("tree")]
    public async Task<IActionResult> list(long? id) =>  (await _getByIdQuery.QueryAsync(id.Value)).ApiResult();

    [HttpGet("treeFalate")]
    public async Task<IActionResult> flate(long? id) =>  (await _getTreeBYIdQuery.QueryAsync(id.Value)).ApiResult();

    [HttpGet("Test")]
    public async Task<IActionResult> test(long? id)
    {
        Expression <Func<Tree, TreeDto>> fun ;
        fun = x => new TreeDto()
        {
            Code = x.Code,
            Description = new NameDto(x.Description.NameAr, x.Description.NameEn),
            Id = x.Id,
            ParentId = x.ParentId,
            FlateParent = x.FlateParent.Select(f => new TreeDto
            {
                Description = new NameDto(f.Parent.Description.NameAr, f.Parent.Description.NameEn),
                Id = f.ParentId,
                ParentId = f.GrandParentId,
            }).ToList()

        };
        //(long ID, Expression<Func<Tree, TreeDto>> func)  = (id.Value, fun);

        return (await _getTreeBYIdQuery.QueryAsync((id.Value, fun))).ApiResult();
    }
}
