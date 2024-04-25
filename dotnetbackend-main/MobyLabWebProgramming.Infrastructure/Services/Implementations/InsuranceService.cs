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

public class InsuranceService : IInsuranceService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;


    public InsuranceService(IRepository<WebAppDatabaseContext> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ServiceResponse> AddInsurance(InsuranceAddDTO insuranceAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins and personnel can add insurances", ErrorCodes.CannotAdd));
        }

        var car = await _repository.GetAsync<Car>(insuranceAddDTO.CarId, cancellationToken);

        if (car == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Car not found", ErrorCodes.EntityNotFound));
        }

        var result = await _repository.GetAsync(new InsuranceSpec(insuranceAddDTO.PolicyNumber), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceAddPermission);
        }

        if (insuranceAddDTO.StartDate >= insuranceAddDTO.EndDate)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceAlreadyExists);
        }

        await _repository.AddAsync(new Insurance
        {
            StartDate = insuranceAddDTO.StartDate,
            EndDate = insuranceAddDTO.EndDate,
            PolicyNumber = insuranceAddDTO.PolicyNumber,
            InsuranceCompany = insuranceAddDTO.InsuranceCompany,
            Price = insuranceAddDTO.Price,
            CarId = insuranceAddDTO.CarId
        }, cancellationToken);
    
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateInsurance(InsuranceUpdateDTO insuranceUpdateDTO, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceUpdatePermission);
        }

        var insurance = await _repository.GetAsync<Insurance>(insuranceUpdateDTO.Id, cancellationToken);

        if (insurance == null)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceNotFound);
        }

        insurance.StartDate = insuranceUpdateDTO.StartDate ?? insurance.StartDate;
        insurance.EndDate = insuranceUpdateDTO.EndDate ?? insurance.EndDate;
        insurance.InsuranceCompany = insuranceUpdateDTO.InsuranceCompany ?? insurance.InsuranceCompany;
        insurance.Price = insuranceUpdateDTO.Price ?? insurance.Price;

        await _repository.UpdateAsync(insurance, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteInsurance(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceDeletePermission);
        }

        var insurance = await _repository.GetAsync<Insurance>(id, cancellationToken);

        if (insurance == null)
        {
            return ServiceResponse.FromError(CommonErrors.InsuranceNotFound);
        }

        await _repository.DeleteAsync<Insurance>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<InsuranceDTO>> GetInsurance(Guid id, CancellationToken cancellationToken = default)
    {
        var insurance = await _repository.GetAsync(new InsuranceProjectionSpec(id, "Insurance"), cancellationToken);

        return insurance != null ? ServiceResponse<InsuranceDTO>.ForSuccess(insurance)
            : ServiceResponse<InsuranceDTO>.FromError(new(HttpStatusCode.NotFound, "Insurance not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var insurances = await _repository.PageAsync(pagination, new InsuranceProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<InsuranceDTO>>.ForSuccess(insurances);
    }

    public async Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurancesByCarId(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(pagination.Search, out _))
        {
            return ServiceResponse<PagedResponse<InsuranceDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid car id", ErrorCodes.InvalidSearchQuery));
        }

        Guid guid = Guid.Parse(pagination.Search);

        var insurances = await _repository.PageAsync(pagination, new InsuranceProjectionSpec(guid, "Car"), cancellationToken);

        return ServiceResponse<PagedResponse<InsuranceDTO>>.ForSuccess(insurances);
    }

    public async Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurancesByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<InsuranceDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }

        var insurances = await _repository.PageAsync(pagination, new InsuranceProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<InsuranceDTO>>.ForSuccess(insurances);
    }
}
