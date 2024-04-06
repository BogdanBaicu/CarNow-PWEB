namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record InsuranceUpdateDTO(
       Guid Id,
          DateOnly? StartDate = default,
             DateOnly? EndDate = default,
                string? PolicyNumber = default,
                   string? InsuranceCompany = default,
                      decimal? Price = default);
