using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class RequestProjectionSpec : BaseSpec<RequestProjectionSpec, Request, RequestDTO>
{
    protected override Expression<Func<Request, RequestDTO>> Spec => e => new()
    {
        Id = e.Id,
        Status = e.Status,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        Price = e.Price,
        Car = e.Car,
        Customer = e.Customer,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public RequestProjectionSpec(Guid id, string search)
    {
        if (search == "Request")
        {
            Query.Where(e => e.Id == id);
        }
        if (search == "Car")
        {
            Query.Where(e => e.Car.Id == id);
        }
        if (search == "Customer")
        {
            Query.Where(e => e.Customer.Id == id);
        }
    }

    public RequestProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";
        
        Query.Where(e => EF.Functions.Like(e.Status, searchExpr) ||
                 EF.Functions.Like(e.Price.ToString(), searchExpr) ||
                 EF.Functions.Like(e.Car.Brand, searchExpr) ||
                 EF.Functions.Like(e.Car.Model, searchExpr) ||
                 EF.Functions.Like(e.Car.LicensePlate, searchExpr) ||
                 EF.Functions.Like(e.Car.Transmission, searchExpr) ||
                 EF.Functions.Like(e.Car.BodyType, searchExpr) ||
                 EF.Functions.Like(e.Car.FuelType, searchExpr) ||
                 EF.Functions.Like(e.Car.Color, searchExpr) ||
                 EF.Functions.Like(e.Customer.Name, searchExpr));
    }
}