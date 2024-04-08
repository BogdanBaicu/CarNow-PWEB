using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IInsuranceService _insuranceService;
    private readonly ICarService _carService;
    private readonly IRequestService _requestService;

    public ReservationService(IRepository<WebAppDatabaseContext> repository, IUserService userService, IMaintenanceService maintenanceService, IInsuranceService insuranceService, ICarService carService, IRequestService requestService)
    {
        _repository = repository;
        _userService = userService;
        _maintenanceService = maintenanceService;
        _insuranceService = insuranceService;
        _carService = carService;
        _requestService = requestService;

    }

    public async Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var employee = await _repository.GetAsync<User>(reservationAddDTO.EmployeeId, cancellationToken);
        if (employee == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Employee not found", ErrorCodes.EntityNotFound));
        }
        
        var request = await _repository.GetAsync<Request>(reservationAddDTO.RequestId, cancellationToken);
        if (request == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Request not found", ErrorCodes.EntityNotFound));
        }

        await _repository.AddAsync(new Reservation
        {
            Status = reservationAddDTO.Status,
            Employee = employee,
            Request = request
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateReservation(ReservationUpdateDTO reservationUpdateDTO, UserDTO requestingtUser, CancellationToken cancellationToken = default)
    {
        var reservation = await _repository.GetAsync<Reservation>(reservationUpdateDTO.Id, cancellationToken);

        if (reservation == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Reservation not found", ErrorCodes.EntityNotFound));
        }

        reservation.Status = reservationUpdateDTO.Status ?? reservation.Status;

        await _repository.UpdateAsync(reservation, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default)
    {
        var reservation = await _repository.GetAsync(new ReservationProjectionSpec(id, "Reservation"), cancellationToken);
        return reservation != null ?  ServiceResponse<ReservationDTO>.ForSuccess(reservation)
            : ServiceResponse<ReservationDTO>.FromError(new(HttpStatusCode.NotFound, "Reservation not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetReservations(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var reservations = await _repository.PageAsync(pagination, new ReservationProjectionSpec(pagination.Search), cancellationToken);
    
        return ServiceResponse<PagedResponse<ReservationDTO>>.ForSuccess(reservations);
    }

    public async Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetReservationsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<ReservationDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }

        var reservations = await _repository.PageAsync(pagination, new ReservationProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ReservationDTO>>.ForSuccess(reservations);
    }
}
