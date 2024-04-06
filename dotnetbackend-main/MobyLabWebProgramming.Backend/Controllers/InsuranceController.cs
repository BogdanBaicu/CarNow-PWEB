using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class InsuranceController : AuthorizedController
{
    private readonly IInsuranceService _insuranceService;

    public InsuranceController(IUserService userService, IInsuranceService insuranceService) : base(userService)
    {
        _insuranceService = insuranceService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] InsuranceAddDTO insuranceAddDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ? 
            this.FromServiceResponse(await _insuranceService.AddInsurance(insuranceAddDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] InsuranceUpdateDTO insuranceUpdateDTO)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _insuranceService.UpdateInsurance(insuranceUpdateDTO, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _insuranceService.DeleteInsurance(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<InsuranceDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _insuranceService.GetInsurance(id)) :
            this.ErrorMessageResult<InsuranceDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<InsuranceDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
           this.FromServiceResponse(await _insuranceService.GetInsurances(pagination)) :
           this.ErrorMessageResult<PagedResponse<InsuranceDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<InsuranceDTO>>>> GetPageByCarId([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _insuranceService.GetInsurancesByCarId(pagination)) :
            this.ErrorMessageResult<PagedResponse<InsuranceDTO>>(currentUser.Error);
    }

}
