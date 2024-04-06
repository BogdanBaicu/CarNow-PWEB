using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class InsuranceProjectionSpec : BaseSpec<InsuranceProjectionSpec, Insurance, InsuranceDTO>
{
    protected override Expression<Func<Insurance, InsuranceDTO>> Spec => e => new()
    {
        Id = e.Id,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        PolicyNumber = e.PolicyNumber,
        InsuranceCompany = e.InsuranceCompany,
        Price = e.Price,
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

    public InsuranceProjectionSpec(Guid id, string search)
    {
        if (search == "Insurance")
        {
            Query.Where(e => e.Id == id);
        }
        if (search == "Car")
        {
            Query.Where(e => e.Car.Id == id);
        }
    }

    public InsuranceProjectionSpec(string? search)
    {
        search =! string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.Like(e.PolicyNumber, searchExpr) ||
                                EF.Functions.Like(e.InsuranceCompany, searchExpr) ||
                                                        EF.Functions.Like(e.Car.Brand, searchExpr) ||
                                                                                EF.Functions.Like(e.Car.Model, searchExpr) ||
                                                                                                        EF.Functions.Like(e.Car.LicensePlate, searchExpr) ||
                                                                                                                                EF.Functions.Like(e.Car.VIN, searchExpr));
    }
}

