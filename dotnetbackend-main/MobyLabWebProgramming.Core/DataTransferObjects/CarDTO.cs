using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CarDTO
{
    public Guid Id { get; set; }
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
    public string FuelType { get; set; } = default!;
    public string BodyType { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public ICollection<Insurance> Insurance { get; set; } = new List<Insurance>();
    public ICollection<Maintenance> Maintenance { get; set; } = new List<Maintenance>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}