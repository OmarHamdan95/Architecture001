namespace Architecture.Model;

public class NameDto
{
    public NameDto()
    {

    }

    public NameDto(string nameAr, string nameEn)
    {
        NameAr = nameAr;
        NameEn = nameEn;
    }
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }

    public string? Value
    {
        get
        {
            return NameAr;
        }
    }
}
