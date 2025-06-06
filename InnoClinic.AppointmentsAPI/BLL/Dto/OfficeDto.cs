namespace BLL.Dto;

public sealed class OfficeDto
{
    public required string Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
}