using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

[JsonConverter(typeof(SmartEnumNameConverter<CarFuelTypeEnum, string>))]
public sealed class CarFuelTypeEnum : SmartEnum<CarFuelTypeEnum, string>
{
    public static readonly CarFuelTypeEnum Electric = new(nameof(Electric), "Electric");
    public static readonly CarFuelTypeEnum Diesel = new(nameof(Diesel), "Diesel");
    public static readonly CarFuelTypeEnum Petrol = new(nameof(Petrol), "Petrol");

    private CarFuelTypeEnum(string name, string value) : base(name, value)
    {
    }
}
