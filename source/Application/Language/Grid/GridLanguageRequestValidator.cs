namespace Architecture.Application;

public sealed class GridLanguageRequestValidator : AbstractValidator<GridLanguageRequest>
{
    public GridLanguageRequestValidator() => RuleFor(request => request).Grid();
}
