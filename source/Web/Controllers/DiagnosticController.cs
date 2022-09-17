using DotNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Architecture.Database.Queries;
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

    public DiagnosticController(IGetByIdQuery<Tree, TreeDto> getByIdQuery)
    {
        _getByIdQuery = getByIdQuery;
    }

    [HttpGet("datetime")]
    public DateTime DateTime() => Assembly.GetExecutingAssembly().FileInfo().LastWriteTime;

    [HttpGet("tree")]
    public async Task<IActionResult> list(long? id) =>  (await _getByIdQuery.QueryAsync(id.Value)).ApiResult();
}
