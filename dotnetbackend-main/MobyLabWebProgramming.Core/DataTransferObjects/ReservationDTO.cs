using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationDTO
{
    public Guid Id { get; set; }
    public ReservationStatusEnum Status { get; set; } = default!;   
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User Employee { get; set; } = default!;
    public Request Request { get; set; } = default!;
}