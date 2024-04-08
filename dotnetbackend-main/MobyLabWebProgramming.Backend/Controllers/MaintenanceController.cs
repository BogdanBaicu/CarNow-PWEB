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

public class MaintenanceController : AuthorizedController
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceController(IUserService userService, IMaintenanceService maintenanceService) : base(userService)
    {
        _maintenanceService = maintenanceService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] MaintenanceAddDTO maintenanceAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ? 
            this.FromServiceResponse(await _maintenanceService.AddMaintenance(maintenanceAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] MaintenanceUpdateDTO maintenanceUpdateDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _maintenanceService.UpdateMaintenance(maintenanceUpdateDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _maintenanceService.DeleteMaintenance(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<MaintenanceDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _maintenanceService.GetMaintenance(id)) :
            this.ErrorMessageResult<MaintenanceDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<MaintenanceDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _maintenanceService.GetMaintenances(pagination)) :
           this.ErrorMessageResult<PagedResponse<MaintenanceDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<MaintenanceDTO>>>> GetPageByCarId([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _maintenanceService.GetMaintenancesByCarId(pagination)) :
            this.ErrorMessageResult<PagedResponse<MaintenanceDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<MaintenanceDTO>>>> GetPageByDetails([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _maintenanceService.GetMaintenancesByDetails(pagination)) :
            this.ErrorMessageResult<PagedResponse<MaintenanceDTO>>(currentUser.Error);
    }
}