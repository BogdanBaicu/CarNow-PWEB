using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IRequestService
{
    public Task<ServiceResponse> AddRequest(RequestAddDTO RequestAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateRequest(RequestUpdateDTO RequestUpdateDTO, UserDTO requestingtUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<RequestDTO>> GetRequest(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<RequestDTO>>> GetRequests(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<RequestDTO>>> GetRequestsByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
