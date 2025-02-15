﻿using Mapster;
namespace Architecture.Application;
using static System.Net.HttpStatusCode;

public sealed record AddDynamicFieldHandler : IRequestHandler<AddDynamicFieldRequest, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDynamicFieldRepository _dynamicFieldRepository;


    public AddDynamicFieldHandler
    (
        IUnitOfWork unitOfWork,
        IDynamicFieldRepository dynamicFieldRepository
    )
    {
        _unitOfWork = unitOfWork;
        _dynamicFieldRepository = dynamicFieldRepository;
    }

    public async Task<Result<long>> Handle(AddDynamicFieldRequest request , CancellationToken cancellationToken)
    {

        var dynamicField = new DynamicField(request.Name.Adapt<Translation>(), request.EntityType.Adapt<LookupValue>() ,
            request.DynamicFieldType.Adapt<LookupValue>(),request.DynamicFieldRegex,request.DynamicFieldMinValue ,request.DynamicFieldMaxValue,
            request.DynamicFieldLookupType.Adapt<LookupValue>());

        await _dynamicFieldRepository.AddAsync(dynamicField);

        await _unitOfWork.SaveChangesAsync();

        return new Result<long>(Created, dynamicField.Id);
       // throw new NotImplementedException();
    }
}
