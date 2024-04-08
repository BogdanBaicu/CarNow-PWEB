using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IInsuranceService
{
    public Task<ServiceResponse> AddInsurance(InsuranceAddDTO insuranceAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateInsurance(InsuranceUpdateDTO insuranceUpdateDTO, UserDTO requestingtUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteInsurance(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<InsuranceDTO>> GetInsurance(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurances(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurancesByCarId(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<InsuranceDTO>>> GetInsurancesByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
