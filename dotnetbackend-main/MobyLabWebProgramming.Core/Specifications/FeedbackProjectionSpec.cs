using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class FeedbackProjectionSpec : BaseSpec<FeedbackProjectionSpec, Feedback, FeedbackDTO>
{
    protected override Expression<Func<Feedback, FeedbackDTO>> Spec => e => new()
    {
        Id = e.Id,
        Description = e.Description,
        CarRating = e.CarRating,
        EmployeeRating = e.EmployeeRating,
        Reservation = e.Reservation,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public FeedbackProjectionSpec(Guid id, string search)
    {
        if (search == "Feedback")
        {
            Query.Where(e => e.Id == id);
        }
        if (search == "Reservation")
        {
            Query.Where(e => e.Reservation.Id == id);
        }
        if (search == "Employee")
        {
            Query.Where(e => e.Reservation.Employee.Id == id);
        }
        if (search == "Car")
        {
            Query.Where(e => e.Reservation.Request.Car.Id == id);
        }
        if (search == "Customer")
        {
            Query.Where(e => e.Reservation.Request.Customer.Id == id);
        }
    }

    public FeedbackProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        // add where query to search by employee rating, car rating, and employee name of the reservation, car brand, car model, car license plate, car vin of the resrevation
        Query.Where(e => EF.Functions.Like(e.EmployeeRating.ToString(), searchExpr) ||
                                       EF.Functions.Like(e.CarRating.ToString(), searchExpr) ||
                                                                                              EF.Functions.Like(e.Reservation.Request.Car.Brand, searchExpr) ||
                                                                                                                                                             EF.Functions.Like(e.Reservation.Request.Car.Model, searchExpr) ||
                                                                                                                                                                                                                                    EF.Functions.Like(e.Reservation.Request.Car.LicensePlate, searchExpr) ||
                                                                                                                                                                                                                                                                                                                   EF.Functions.Like(e.Reservation.Request.Car.VIN, searchExpr) ||
                                                                                                                                                                                                                                                                                                                                                                                                          EF.Functions.Like(e.Reservation.Employee.Name, searchExpr));
        
    }
}