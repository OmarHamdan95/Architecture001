namespace Architecture.Application;

public sealed class GridLookupRequestValidator : AbstractValidator<GridLookupRequest>
{
    public GridLookupRequestValidator() => RuleFor(request => request).Grid();
}
