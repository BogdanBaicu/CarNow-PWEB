using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeedbackService
{
    public Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedbackAddDTO, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacksByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
