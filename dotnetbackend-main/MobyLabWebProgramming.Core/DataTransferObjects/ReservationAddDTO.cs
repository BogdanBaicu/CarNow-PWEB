using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationAddDTO
{
    public ReservationStatusEnum Status { get; set; } = ReservationStatusEnum.Pending;
    public Guid EmployeeId { get; set; } = default!;
    public Guid RequestId { get; set; } = default!;
}
