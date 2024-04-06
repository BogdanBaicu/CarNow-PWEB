using MobyLabWebProgramming.Core.Enums;
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackAddDTO
{

    public string Description { get; set; } = default!;
    public int CarRating { get; set; } = default!;
    public int EmployeeRating { get; set; } = default!;

}
