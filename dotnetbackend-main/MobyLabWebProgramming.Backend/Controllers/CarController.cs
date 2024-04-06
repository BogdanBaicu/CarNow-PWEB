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

public class CarController : AuthorizedController
{
    private readonly ICarService _carService;
    private readonly IInsuranceService _insuranceService;
    private readonly IMaintenanceService _maintenanceService;

    public CarController(IUserService userService, ICarService carService, IInsuranceService insuranceService, IMaintenanceService maintenanceService) : base(userService)
    {
        _carService = carService;
        _insuranceService = insuranceService;
        _maintenanceService = maintenanceService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CarAddDTO carAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _carService.AddCar(carAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CarUpdateDTO carUpdateDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _carService.UpdateCar(carUpdateDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _carService.DeleteCar(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<CarDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _carService.GetCar(id)) :
            this.ErrorMessageResult<CarDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<CarDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _carService.GetCars(pagination)) :
           this.ErrorMessageResult<PagedResponse<CarDTO>>(currentUser.Error);
    }

}
