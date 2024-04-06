namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CarUpdateDTO(
    Guid Id,
        string? Brand = default,
            string? Model = default,
                string? LicensePlate = default,
                    string? VIN = default,
                        int? Year = default,
                            string? Color = default,
                                string? Transmission = default,
                                    int? EngineCC = default,
                                        int? PowerHP = default,
                                            decimal? Price = default,
                                                string? FuelType = default,
                                                    string? BodyType = default,
                                                        string? ImageUrl = default);