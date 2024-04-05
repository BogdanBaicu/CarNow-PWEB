using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

/// <summary>
/// This is and example of a smart enum, you can modify it however you see fit.
/// Note that the class is decorated with a JsonConverter attribute so that it is properly serialized as a JSON.
/// </summary>
[JsonConverter(typeof(SmartEnumNameConverter<ReservationStatusEnum, string>))]
public sealed class ReservationStatusEnum : SmartEnum<ReservationStatusEnum, string>
{
    public static readonly ReservationStatusEnum Pending = new(nameof(Pending), "Pending");
    public static readonly ReservationStatusEnum Confirmed = new(nameof(Confirmed), "Confirmed");
    public static readonly ReservationStatusEnum Canceled = new(nameof(Canceled), "Canceled");

    private ReservationStatusEnum(string name, string value) : base(name, value)
    {
    }
}
