using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IMaintenanceService
{
    public Task<ServiceResponse> AddMaintenance(MaintenanceAddDTO maintenanceAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateMaintenance(MaintenanceUpdateDTO maintenanceUpdateDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteMaintenance(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<MaintenanceDTO>> GetMaintenance(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenancesByCarId(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<MaintenanceDTO>>> GetMaintenancesByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task GetMaintenancesByCarId(Guid carId, CancellationToken cancellationToken);
}
