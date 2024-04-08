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

public class RequestService : IRequestService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IInsuranceService _insuranceService;
    private readonly ICarService _carService;


    public RequestService(IRepository<WebAppDatabaseContext> repository, IUserService userService, IMaintenanceService maintenanceService, IInsuranceService insuranceService, ICarService carService)
    {
        _repository = repository;
        _userService = userService;
        _maintenanceService = maintenanceService;
        _insuranceService = insuranceService;
        _carService = carService;
    }

    public async Task<ServiceResponse> AddRequest(RequestAddDTO requestAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var car = await _repository.GetAsync<Car>(requestAddDTO.CarId, cancellationToken);
        if (car == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
        }
        
        var customer = await _repository.GetAsync<User>(requestAddDTO.CustomerId, cancellationToken);
        if (customer == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Customer not found", ErrorCodes.EntityNotFound));
        }

        if (requestAddDTO.StartDate > requestAddDTO.EndDate)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Start date must be before end date", ErrorCodes.InvalidDateRange));
        }

        await _repository.AddAsync(new Request
        {
            Status = requestAddDTO.Status,
            StartDate = requestAddDTO.StartDate,
            EndDate = requestAddDTO.EndDate,
            Price = car.Price * ( requestAddDTO.EndDate.DayNumber - requestAddDTO.StartDate.DayNumber + 1),
            Car = car,
            Customer = customer
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateRequest(RequestUpdateDTO requestUpdateDTO, UserDTO requestingtUser, CancellationToken cancellationToken = default)
    {
        var request = await _repository.GetAsync<Request>(requestUpdateDTO.Id, cancellationToken);

        if (request == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Request not found", ErrorCodes.EntityNotFound));
        }

        request.Status = requestUpdateDTO.Status ?? request.Status;

        await _repository.UpdateAsync(request, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<RequestDTO>> GetRequest(Guid id, CancellationToken cancellationToken = default)
    {
        var request = await _repository.GetAsync(new RequestProjectionSpec(id, "Request"), cancellationToken);
        return request != null ? ServiceResponse<RequestDTO>.ForSuccess(request)
            : ServiceResponse<RequestDTO>.FromError(new(HttpStatusCode.NotFound, "Request not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<RequestDTO>>> GetRequests(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var requests = await _repository.PageAsync(pagination, new RequestProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<RequestDTO>>.ForSuccess(requests);
    }

    public async Task<ServiceResponse<PagedResponse<RequestDTO>>> GetRequestsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<RequestDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }

        var Requests = await _repository.PageAsync(pagination, new RequestProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<RequestDTO>>.ForSuccess(Requests);
    }
}
