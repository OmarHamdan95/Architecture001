using Architecture.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Architecture.Web.ActionFilter;

public class UnitOfWorkActionFilter :  IAsyncActionFilter
{
    private readonly IAsyncUnitOfWork _unitOfWork;

    public UnitOfWorkActionFilter(IAsyncUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var result = await next();

         if (result.Exception != null )
            await _unitOfWork.EndAsync();
        else
            await _unitOfWork.Rollback();
    }

   // private bool IsValidStatusCode()
}
