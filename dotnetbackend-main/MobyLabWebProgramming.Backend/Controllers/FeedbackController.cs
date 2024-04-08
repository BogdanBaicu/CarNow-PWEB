using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class FeedbackController : AuthorizedController
{
    private readonly ICarService _carService;
    private readonly IInsuranceService _insuranceService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IReservationService _reservationService;
    private readonly IFeedbackService _feedbackService;
    private readonly IRequestService _requestService;

    public FeedbackController(IUserService userService, ICarService carService, IInsuranceService insuranceService, IMaintenanceService maintenanceService, IReservationService reservationService, IFeedbackService feedbackService, IRequestService requestService) : base(userService)
    {
        _carService = carService;
        _insuranceService = insuranceService;
        _maintenanceService = maintenanceService;
        _reservationService = reservationService;
        _feedbackService = feedbackService;
        _requestService = requestService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] FeedbackAddDTO feedbackAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _feedbackService.AddFeedback(feedbackAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<FeedbackDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _feedbackService.GetFeedback(id)) :
            this.ErrorMessageResult<FeedbackDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<FeedbackDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _feedbackService.GetFeedbacks(pagination)) :
           this.ErrorMessageResult<PagedResponse<FeedbackDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<FeedbackDTO>>>> GetFeedbacksByDetails([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _feedbackService.GetFeedbacksByDetails(pagination)) :
            this.ErrorMessageResult<PagedResponse<FeedbackDTO>>(currentUser.Error);
    }
}
