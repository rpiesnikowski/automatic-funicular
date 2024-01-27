namespace AbaxService.Validation;

public record TrainValidationResult(bool IsValid, string ValidationMessage)
{
    public TrainValidationResult(bool isValid) : this(isValid, string.Empty)
    {
    }
}