using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

[JsonConverter(typeof(SmartEnumNameConverter<CarBodyTypeEnum, string>))]
public sealed class CarBodyTypeEnum : SmartEnum<CarBodyTypeEnum, string>
{
    public static readonly CarBodyTypeEnum Coupe = new(nameof(Coupe), "Coupe");
    public static readonly CarBodyTypeEnum SUV = new(nameof(SUV), "SUV");
    public static readonly CarBodyTypeEnum Convertible = new(nameof(Convertible), "Convertible");
    public static readonly CarBodyTypeEnum Sedan = new(nameof(Sedan), "Sedan");
    public static readonly CarBodyTypeEnum Van = new(nameof(Van), "Van");
    public static readonly CarBodyTypeEnum Break = new(nameof(Break), "Break");
    public static readonly CarBodyTypeEnum Hatchback = new(nameof(Hatchback), "Hatchback");

    private CarBodyTypeEnum(string name, string value) : base(name, value)
    {
    }
}
