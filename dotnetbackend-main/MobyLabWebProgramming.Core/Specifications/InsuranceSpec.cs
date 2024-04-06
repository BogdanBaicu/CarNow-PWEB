using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class InsuranceSpec : BaseSpec<InsuranceSpec, Insurance>
{
    public InsuranceSpec(Guid id) : base(id)
    {
    }

    public InsuranceSpec(string policyNumber)
    {
        Query.Where(e => e.PolicyNumber == policyNumber);
    }
}