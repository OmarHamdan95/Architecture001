namespace Architecture.Application;

public sealed record AuthRequest(string Login, string Password) : IRequest<Result<AuthResponse>>;
