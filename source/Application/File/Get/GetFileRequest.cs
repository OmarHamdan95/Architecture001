namespace Architecture.Application;

public sealed record GetFileRequest(Guid Id) : IRequest<Result<BinaryFile>>;
