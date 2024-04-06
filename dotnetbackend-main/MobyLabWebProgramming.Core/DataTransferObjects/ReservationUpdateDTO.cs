using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ReservationUpdateDTO(
       Guid Id,
          ReservationStatusEnum? Status = default);