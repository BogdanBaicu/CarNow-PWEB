using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReservationProjectionSpec : BaseSpec<ReservationProjectionSpec, Reservation, ReservationDTO>
{
    protected override Expression<Func<Reservation, ReservationDTO>> Spec => e => new()
    {
        Id = e.Id,
        Status = e.Status,
        Employee = e.Employee,
        Request = e.Request,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public ReservationProjectionSpec(Guid id, string search)
    {
        if (search == "Reservation")
        {
            Query.Where(e => e.Id == id);
        }
        if (search == "Employee")
        {
            Query.Where(e => e.Employee.Id == id);
        }
        if (search == "Car")
        {
            Query.Where(e => e.Request.Car.Id == id);
        }
        if (search == "Customer")
        {
            Query.Where(e => e.Request.Customer.Id == id);
        }
    }

    public ReservationProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";


        Query.Where(e => EF.Functions.Like(e.Status, searchExpr) ||
                                    EF.Functions.Like(e.Request.Price.ToString(), searchExpr) ||
                                                            EF.Functions.Like(e.Request.Car.Brand, searchExpr) ||
                                                                                    EF.Functions.Like(e.Request.Car.Model, searchExpr) ||
                                                                                                            EF.Functions.Like(e.Request.Car.LicensePlate, searchExpr) ||
                                                                                                                                    EF.Functions.Like(e.Request.Car.Transmission, searchExpr) ||
                                                                                                                                                            EF.Functions.Like(e.Request.Car.BodyType, searchExpr) ||
                                                                                                                                                                                    EF.Functions.Like(e.Request.Car.FuelType, searchExpr) ||
                                                                                                                                                                                                            EF.Functions.Like(e.Request.Car.Color, searchExpr) ||
                                                                                                                                                                                                                                    EF.Functions.Like(e.Employee.Name, searchExpr) ||
                                                                                                                                                                                                                                                            EF.Functions.Like(e.Request.Customer.Name, searchExpr));
    }
}