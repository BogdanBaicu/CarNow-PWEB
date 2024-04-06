using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICarService
{
    public Task<ServiceResponse> AddCar(CarAddDTO carAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
}
