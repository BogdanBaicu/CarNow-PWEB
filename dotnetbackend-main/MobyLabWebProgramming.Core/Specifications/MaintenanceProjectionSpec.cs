using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class MaintenanceProjectionSpec : BaseSpec<MaintenanceProjectionSpec, Maintenance, MaintenanceDTO>
{
    protected override Expression<Func<Maintenance, MaintenanceDTO>> Spec => e => new()
    {
        Id = e.Id,
        MaintenanceDate = e.MaintenanceDate,
        Description = e.Description,
        Price = e.Price,
        ServiceName = e.ServiceName,
        CarId = e.CarId,
        Car = new()
        {
            Id = e.Car.Id,
            Brand = e.Car.Brand,
            Model = e.Car.Model,
            LicensePlate = e.Car.LicensePlate,
            VIN = e.Car.VIN,
            Year = e.Car.Year,
            Color = e.Car.Color,
            Transmission = e.Car.Transmission,
            EngineCC = e.Car.EngineCC,
            PowerHP = e.Car.PowerHP,
            Price = e.Car.Price,
            FuelType = e.Car.FuelType,
            BodyType = e.Car.BodyType,
            ImageUrl = e.Car.ImageUrl,
        },
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public MaintenanceProjectionSpec(Guid id, string search)
    {
        if (search == "Maintenance")
        {
            Query.Where(e => e.Id == id);
        }
        if (search == "Car")
        {
            Query.Where(e => e.Car.Id == id);
        }
    }

    public MaintenanceProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.Like(e.Description, searchExpr) || 
                                        EF.Functions.Like(e.ServiceName, searchExpr) || 
                                                            EF.Functions.Like(e.Car.Brand, searchExpr) ||
                                                                                EF.Functions.Like(e.Car.Model, searchExpr) ||
                                                                                                        EF.Functions.Like(e.Car.LicensePlate, searchExpr) ||
                                                                                                                                EF.Functions.Like(e.Car.VIN, searchExpr));
    }
}