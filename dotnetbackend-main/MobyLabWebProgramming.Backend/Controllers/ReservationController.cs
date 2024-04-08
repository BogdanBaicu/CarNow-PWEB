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

public class ReservationController : AuthorizedController
{
    private readonly IReservationService _reservationService;
    private readonly ICarService _carService;
    private readonly IInsuranceService _insuranceService;
    private readonly IMaintenanceService _maintenanceService;
    private readonly IRequestService _requestService;

    public ReservationController(IUserService userService, ICarService carService, IInsuranceService insuranceService, IMaintenanceService maintenanceService, IReservationService reservationService, IRequestService requestService) : base(userService)
    {
        _reservationService = reservationService;
        _carService = carService;
        _insuranceService = insuranceService;
        _maintenanceService = maintenanceService;
        _requestService = requestService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ReservationAddDTO reservationAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.AddReservation(reservationAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ReservationUpdateDTO reservationUpdateDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.UpdateReservation(reservationUpdateDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ReservationDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.GetReservation(id)) :
            this.ErrorMessageResult<ReservationDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ReservationDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _reservationService.GetReservations(pagination)) :
           this.ErrorMessageResult<PagedResponse<ReservationDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ReservationDTO>>>> GetReservationsByDetails([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.GetReservationsByDetails(pagination)) :
            this.ErrorMessageResult<PagedResponse<ReservationDTO>>(currentUser.Error);
    }

}
