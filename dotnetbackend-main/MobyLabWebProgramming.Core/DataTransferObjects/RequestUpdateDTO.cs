using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record RequestUpdateDTO(
       Guid Id,
          ReservationStatusEnum? Status = default);