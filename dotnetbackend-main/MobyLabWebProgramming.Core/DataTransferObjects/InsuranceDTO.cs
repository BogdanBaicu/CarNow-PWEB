using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class InsuranceDTO
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public string PolicyNumber { get; set; } = default!;
    public string InsuranceCompany { get; set; } = default!;
    public decimal Price { get; set; } = default!;



    public Guid CarId { get; set; }
    public Car Car { get; set; } = default!;



    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
