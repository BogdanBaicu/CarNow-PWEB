using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CarProjectionSpec : BaseSpec<CarProjectionSpec, Car, CarDTO>
{
    protected override Expression<Func<Car, CarDTO>> Spec => e => new()
    {
        Id = e.Id,
        Brand = e.Brand,
        Model = e.Model,
        LicensePlate = e.LicensePlate,
        VIN = e.VIN,
        Year = e.Year,
        Color = e.Color,
        Transmission = e.Transmission,
        EngineCC = e.EngineCC,
        PowerHP = e.PowerHP,
        Price = e.Price,
        FuelType = e.FuelType,
        BodyType = e.BodyType,
        ImageUrl = e.ImageUrl,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public CarProjectionSpec(Guid id, string search)
    {
        if (search == "Car")
        {
            Query.Where(e => e.Id == id);
        }
    }

    public CarProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.Like(e.Brand, searchExpr) ||
                                   EF.Functions.Like(e.Model, searchExpr) ||
                                                              EF.Functions.Like(e.LicensePlate, searchExpr) ||
                                                                                         EF.Functions.Like(e.VIN, searchExpr) ||
                                                                                                                    EF.Functions.Like(e.Color, searchExpr) ||
                                                                                                                                               EF.Functions.Like(e.FuelType.ToString(), searchExpr) ||
                                                                                                                                                                          EF.Functions.Like(e.BodyType.ToString(), searchExpr) ||
                                                                                                                                                                                                     EF.Functions.Like(e.Transmission.ToString(), searchExpr) ||
                                                                                                                                                                                                                                EF.Functions.Like(e.EngineCC.ToString(), searchExpr) ||
                                                                                                                                                                                                                                                           EF.Functions.Like(e.PowerHP.ToString(), searchExpr) ||
                                                                                                                                                                                                                                                                                      EF.Functions.Like(e.Year.ToString(), searchExpr) ||
                                                                                                                                                                                                                                                                                                                 EF.Functions.Like(e.Price.ToString(), searchExpr));
    }
}

