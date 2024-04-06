using System.Net;
using System.Threading;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Implementation;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CarService : ICarService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IInsuranceService _insuranceService;

    public CarService(IRepository<WebAppDatabaseContext> repository, IUserService userService, IMaintenanceService maintenanceService, IInsuranceService insuranceService)
    {
        _repository = repository;
        _userService = userService;
        _maintenanceService = maintenanceService;
        _insuranceService = insuranceService;
    }

    public async Task<ServiceResponse> AddCar(CarAddDTO carAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can add cars", ErrorCodes.CannotAdd));
        }

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
            Insurance = new List<Insurance>(),
            Maintenance = new List<Maintenance>(),
            FuelType = carAddDTO.FuelType,
            BodyType = carAddDTO.BodyType,
            ImageUrl = carAddDTO.ImageUrl,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCar(CarUpdateDTO carUpdateDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can update insurances", ErrorCodes.CannotUpdate));
        }

        var car = await _repository.GetAsync<Car>(carUpdateDTO.Id, cancellationToken);

        if (car == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
        }

        car.Brand = carUpdateDTO.Brand ?? car.Brand;
        car.Model = carUpdateDTO.Model ?? car.Model;
        car.LicensePlate = carUpdateDTO.LicensePlate ?? car.LicensePlate;
        car.VIN = carUpdateDTO.VIN ?? car.VIN;
        car.Year = carUpdateDTO.Year ?? car.Year;
        car.Color = carUpdateDTO.Color ?? car.Color;
        car.Transmission = carUpdateDTO.Transmission ?? car.Transmission;
        car.EngineCC = carUpdateDTO.EngineCC ?? car.EngineCC;
        car.PowerHP = carUpdateDTO.PowerHP ?? car.PowerHP;
        car.Price = carUpdateDTO.Price ?? car.Price;
        car.FuelType = (CarFuelTypeEnum)(carUpdateDTO.FuelType ?? car.FuelType);
        car.BodyType = (CarBodyTypeEnum)(carUpdateDTO.BodyType ?? car.BodyType);
        car.ImageUrl = carUpdateDTO.ImageUrl ?? car.ImageUrl;

        await _repository.UpdateAsync(car, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCar(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can delete cars", ErrorCodes.CannotDelete));
        }

        var car = await _repository.GetAsync<Car>(id, cancellationToken);

        if (car == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
        }

        // delete all insurances for the car
        var insurances = await _repository.ListAsync(new InsuranceSpec(car.Id), cancellationToken);
        foreach (var insurance in insurances)
        {
            await _insuranceService.DeleteInsurance(insurance.Id, requestingUser, cancellationToken);
        }

        // delete all maintenances for the car
        var maintenances = await _repository.ListAsync(new MaintenanceSpec(car.Id), cancellationToken);
        foreach (var maintenance in maintenances)
        {
            await _maintenanceService.DeleteMaintenance(maintenance.Id, requestingUser, cancellationToken);
        }

        await _repository.DeleteAsync<Car>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<CarDTO>> GetCar(Guid id, CancellationToken cancellationToken = default)
    {
        var car = await _repository.GetAsync(new CarProjectionSpec(id, "Car"), cancellationToken);
        return car != null ? ServiceResponse<CarDTO>.ForSuccess(car)
            : ServiceResponse<CarDTO>.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<CarDTO>>> GetCars(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var cars = await _repository.PageAsync(pagination, new CarProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse<PagedResponse<CarDTO>>.ForSuccess(cars);
    }

    public async Task<ServiceResponse<PagedResponse<CarDTO>>> GetCarsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<CarDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }
        
        var cars = await _repository.PageAsync(pagination, new CarProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CarDTO>>.ForSuccess(cars);
    }
}
