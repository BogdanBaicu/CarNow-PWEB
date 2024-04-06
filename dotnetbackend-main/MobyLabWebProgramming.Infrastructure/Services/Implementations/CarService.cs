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

public class CarService : ICarService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;

    public CarService(IRepository<WebAppDatabaseContext> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ServiceResponse> AddCar(CarAddDTO carAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can add cars", ErrorCodes.CannotAdd));
        //}

        var result = await _repository.GetAsync(new CarSpec(carAddDTO.VIN), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car with the same license plate already exists", ErrorCodes.CarAlreadyExists));
        }

        await _repository.AddAsync(new Car
        {
            Brand = carAddDTO.Brand,
            Model = carAddDTO.Model,
            LicensePlate = carAddDTO.LicensePlate,
            VIN = carAddDTO.VIN,
            Year = carAddDTO.Year,
            Color = carAddDTO.Color,
            Transmission = carAddDTO.Transmission,
            EngineCC = carAddDTO.EngineCC,
            PowerHP = carAddDTO.PowerHP,
            Price = carAddDTO.Price,
            FuelType = carAddDTO.FuelType,
            BodyType = carAddDTO.BodyType,
            ImageUrl = carAddDTO.ImageUrl,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
