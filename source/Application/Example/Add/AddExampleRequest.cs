namespace Architecture.Application;

public sealed record AddExampleRequest(string Name) : IRequest<Result<long>>;
