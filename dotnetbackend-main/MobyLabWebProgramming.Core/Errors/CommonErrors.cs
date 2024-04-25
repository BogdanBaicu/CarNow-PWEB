using System.Net;
using System.Security;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);

    public static ErrorMessage CarAddPermission => new(HttpStatusCode.Forbidden, "Only admins and personnel can add cars", ErrorCodes.CannotAdd);
    public static ErrorMessage CarUpdatePermission => new(HttpStatusCode.BadRequest, "Only admins and personnel can update cars", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage CarDeletePermission => new(HttpStatusCode.BadRequest, "Only admins can delete cars", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage FeedbackAddPermission => new(HttpStatusCode.Forbidden, "Only users can add feedbacks", ErrorCodes.CannotAdd);
    public static ErrorMessage InsuranceAddPermission => new(HttpStatusCode.Forbidden, "Only admins and personnel can add insurances", ErrorCodes.CannotAdd);
    public static ErrorMessage InsuranceUpdatePermission => new(HttpStatusCode.BadRequest, "Only admins and personnel can update insurances", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage InsuranceDeletePermission => new(HttpStatusCode.BadRequest, "Only admins can delete insurances", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage MaintenanceAddPermission => new(HttpStatusCode.Forbidden, "Only admins and personnel can add maintenances", ErrorCodes.CannotAdd);
    public static ErrorMessage MaintenanceUpdatePermission => new(HttpStatusCode.BadRequest, "Only admins and personnel can update maintenances", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage MaintenanceDeletePermission => new(HttpStatusCode.BadRequest, "Only admins can delete maintenances", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage CarNotFound => new(HttpStatusCode.BadRequest, "Car does not exist", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage CarAlreadyExists => new(HttpStatusCode.BadRequest, "Car already exists", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage InsuranceNotFound => new(HttpStatusCode.BadRequest, "Insurance does not exist", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage InsuranceAlreadyExists => new(HttpStatusCode.BadRequest, "Insurance already exists", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage MaintenanceNotFound => new(HttpStatusCode.BadRequest, "Maintenance does not exist", ErrorCodes.CarAlreadyExists);
    public static ErrorMessage MaintenanceAlreadyExists => new(HttpStatusCode.BadRequest, "Maintenance already exists", ErrorCodes.CarAlreadyExists);
}
