namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class InsuranceAddDTO
{
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public string PolicyNumber { get; set; } = default!;
    public string InsuranceCompany { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid CarId { get; set; }
}