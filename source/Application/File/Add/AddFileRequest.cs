namespace Architecture.Application;

public sealed record AddFileRequest(IEnumerable<BinaryFile> Files) : IRequest<Result<IEnumerable<BinaryFile>>>;
