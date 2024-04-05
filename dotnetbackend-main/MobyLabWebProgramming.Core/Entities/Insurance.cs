namespace MobyLabWebProgramming.Core.Entities;

public class Insurance : BaseEntity
{
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public string PolicyNumber { get; set; } = default!;
    public string InsuranceCompany { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
