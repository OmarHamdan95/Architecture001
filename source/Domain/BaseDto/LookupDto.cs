namespace Architecture.Domain.BaseDto;

public class LookupDto
{
    public LookupDto()
    {

    }

    public LookupDto(Name? description , long? id , string? code)
    {
        this.Description = description;
        this.Id = id;
        this.Code = code;
    }
    public Name? Description { get; set; }

    public string? Code { get; set; }
    public long? Id { get; set; }

    public string? Value => $"{Code} : {Description.NameAr}";
}
