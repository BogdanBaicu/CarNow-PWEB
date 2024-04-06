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

public class FeedbackService : IFeedbackService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IUserService _userService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IInsuranceService _insuranceService;
    private readonly IReservationService _reservationService;

    public FeedbackService(IRepository<WebAppDatabaseContext> repository, IUserService userService, IMaintenanceService maintenanceService, IInsuranceService insuranceService, IReservationService reservationService)
    {
        _repository = repository;
        _userService = userService;
        _maintenanceService = maintenanceService;
        _insuranceService = insuranceService;
        _reservationService = reservationService;
    }

    public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedbackAddDTO, UserDTO requestingUser, CancellationToken cancellationToken)
    {
        if (requestingUser.Role != UserRoleEnum.Client)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only clients can add feedback", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Feedback
        {
            Description = feedbackAddDTO.Description,
            CarRating = feedbackAddDTO.CarRating,
            EmployeeRating = feedbackAddDTO.EmployeeRating,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, CancellationToken cancellationToken = default)
    {
        var feedback = await _repository.GetAsync(new FeedbackProjectionSpec(id, "Feedback"), cancellationToken);
        return feedback != null ? ServiceResponse<FeedbackDTO>.ForSuccess(feedback)
            : ServiceResponse<FeedbackDTO>.FromError(new(HttpStatusCode.BadRequest, "Feedback not found", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken)
    {
        var feedbacks = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(feedbacks);
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacksByDetails(PaginationSearchQueryParams pagination, CancellationToken cancellationToken)
    {
        if (pagination.Search == null)
        {
            return ServiceResponse<PagedResponse<FeedbackDTO>>.FromError(new(HttpStatusCode.BadRequest, "Invalid search query", ErrorCodes.InvalidSearchQuery));
        }

        var feedbacks = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(feedbacks);
    }
}