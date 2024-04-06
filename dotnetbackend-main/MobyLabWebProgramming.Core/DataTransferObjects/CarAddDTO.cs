using MobyLabWebProgramming.Core.Enums;
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CarAddDTO
{

    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string LicensePlate { get; set; } = default!;
    public string VIN { get; set; } = default!;
    public int Year { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Transmission { get; set; } = default!;
    public int EngineCC { get; set; } = default!;
    public int PowerHP { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public CarFuelTypeEnum FuelType { get; set; } = default!;
    public CarBodyTypeEnum BodyType { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
