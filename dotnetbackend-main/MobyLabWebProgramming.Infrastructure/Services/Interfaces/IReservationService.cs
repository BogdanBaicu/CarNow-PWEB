using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReservationService
{
    public Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateReservation(ReservationUpdateDTO reservationUpdateDTO, UserDTO requestingtUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetReservations(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetReservationsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
