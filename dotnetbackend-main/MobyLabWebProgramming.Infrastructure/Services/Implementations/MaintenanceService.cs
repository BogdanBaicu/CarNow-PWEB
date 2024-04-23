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

public class MaintenanceService : IMaintenanceService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;


    public MaintenanceService(IRepository<WebAppDatabaseContext> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ServiceResponse> AddMaintenance(MaintenanceAddDTO maintenanceAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can add maintenances", ErrorCodes.CannotAdd));
        //}

        var car = await _repository.GetAsync<Car>(maintenanceAddDTO.CarId, cancellationToken);

        if (car == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
        }

        await _repository.AddAsync(new Maintenance
        {
            MaintenanceDate = maintenanceAddDTO.MaintenanceDate,
            Description = maintenanceAddDTO.Description,
            Price = maintenanceAddDTO.Price,
            ServiceName = maintenanceAddDTO.ServiceName,
            CarId = maintenanceAddDTO.CarId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateMaintenance(MaintenanceUpdateDTO maintenanceUpdateDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can update maintenances", ErrorCodes.CannotUpdate));
        //}

        var maintenance = await _repository.GetAsync<Maintenance>(maintenanceUpdateDTO.Id, cancellationToken);

        if (maintenance == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Maintenance not found", ErrorCodes.EntityNotFound));
        }

        maintenance.MaintenanceDate = maintenanceUpdateDTO.MaintenanceDate ?? maintenance.MaintenanceDate;
        maintenance.Description = maintenanceUpdateDTO.Description ?? maintenance.Description;
        maintenance.Price = maintenanceUpdateDTO.Price ?? maintenance.Price;
        maintenance.ServiceName = maintenanceUpdateDTO.ServiceName ?? maintenance.ServiceName;

        await _repository.UpdateAsync(maintenance, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteMaintenance(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingUser.Role != UserRoleEnum.Admin)
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can delete maintenances", ErrorCodes.CannotDelete));
        //}

        var maintenance = await _repository.GetAsync<Maintenance>(id, cancellationToken);

        if (maintenance == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Maintenance not found", ErrorCodes.EntityNotFound));
        }

        await _repository.DeleteAsync<Maintenance>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<MaintenanceDTO>> GetMaintenance(Guid id, CancellationToken cancellationToken = default)
    {
        var maintenance = await _repository.GetAsync(new MaintenanceProjectionSpec(id, "Maintenance"), cancellationToken);

        return maintenance != null ? ServiceResponse<MaintenanceDTO>.ForSuccess(maintenance)
            : ServiceResponse<MaintenanceDTO>.FromError(new(HttpStatusCode.NotFound, "Maintenance not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var maintenances = await _repository.PageAsync(pagination, new MaintenanceProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<MaintenanceDTO>>.ForSuccess(maintenances);
    }

    public async Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenancesByCarId(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(pagination.Search, out _))
        {
            return ServiceResponse<PagedResponse<MaintenanceDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid car id", ErrorCodes.InvalidSearchQuery));
        }

        Guid guid = Guid.Parse(pagination.Search);

        var maintenances = await _repository.PageAsync(pagination, new MaintenanceProjectionSpec(guid, "Car"), cancellationToken);

        return ServiceResponse<PagedResponse<MaintenanceDTO>>.ForSuccess(maintenances);
    }

    public async Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenancesByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<MaintenanceDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }

        var maintenances = await _repository.PageAsync(pagination, new MaintenanceProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<MaintenanceDTO>>.ForSuccess(maintenances);
    }
}
