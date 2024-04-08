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
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        Price = e.Price,
        Car = e.Car,
        Employee = e.Employee,
        Customer = e.Customer,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public ReservationProjectionSpec(Guid id, string search)
    {
        if (search == "Reservation")
        {
            Query.Where(e => e.Id == id);
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

        // add where query to search by status, start date, end date, price, car brand, car model, car license plate, car trasmission, car body type, car fuel type, car color, employee name, customer name

        Query.Where(e => EF.Functions.Like(e.Status.ToString(), searchExpr) ||
                                EF.Functions.Like(e.StartDate.ToString(), searchExpr) ||
                                                        EF.Functions.Like(e.EndDate.ToString(), searchExpr) ||
                                                                                EF.Functions.Like(e.Price.ToString(), searchExpr) ||
                                                                                                        EF.Functions.Like(e.Car.Brand, searchExpr) ||
                                                                                                                                EF.Functions.Like(e.Car.Model, searchExpr) ||
                                                                                                                                                        EF.Functions.Like(e.Car.LicensePlate, searchExpr) ||
                                                                                                                                                                                EF.Functions.Like(e.Car.Transmission, searchExpr) ||
                                                                                                                                                                                                        EF.Functions.Like(e.Car.BodyType, searchExpr) ||
                                                                                                                                                                                                                                EF.Functions.Like(e.Car.FuelType, searchExpr) ||
                                                                                                                                                                                                                                                        EF.Functions.Like(e.Car.Color, searchExpr) ||
                                                                                                                                                                                                                                                                                EF.Functions.Like(e.Employee.Name, searchExpr) ||
                                                                                                                                                                                                                                                                                                        EF.Functions.Like(e.Customer.Name, searchExpr));
    }
}