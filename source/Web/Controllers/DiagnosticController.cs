using DotNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Architecture.Database.Queries;
using Architecture.Database.Queries.QueriesCustome;
using Architecture.Domain;
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
}
