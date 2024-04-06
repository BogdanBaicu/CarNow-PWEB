using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICarService
{
    public Task<ServiceResponse> AddCar(CarAddDTO carAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateCar(CarUpdateDTO carUpdateDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteCar(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<CarDTO>> GetCar(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<CarDTO>>> GetCars(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<CarDTO>>> GetCarsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
