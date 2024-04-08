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

public class RequestController : AuthorizedController
{
    private readonly IRequestService _requestService;
    private readonly ICarService _carService;
    private readonly IInsuranceService _insuranceService;
    private readonly IMaintenanceService _maintenanceService;

    public RequestController(IUserService userService, ICarService carService, IInsuranceService insuranceService, IMaintenanceService maintenanceService, IRequestService requestService) : base(userService)
    {
        _requestService = requestService;
        _carService = carService;
        _insuranceService = insuranceService;
        _maintenanceService = maintenanceService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] RequestAddDTO RequestAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _requestService.AddRequest(RequestAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] RequestUpdateDTO RequestUpdateDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _requestService.UpdateRequest(RequestUpdateDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<RequestDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _requestService.GetRequest(id)) :
            this.ErrorMessageResult<RequestDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<RequestDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _requestService.GetRequests(pagination)) :
           this.ErrorMessageResult<PagedResponse<RequestDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<RequestDTO>>>> GetRequestsByDetails([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _requestService.GetRequestsByDetails(pagination)) :
            this.ErrorMessageResult<PagedResponse<RequestDTO>>(currentUser.Error);
    }

}
