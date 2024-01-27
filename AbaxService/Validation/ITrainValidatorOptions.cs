namespace AbaxService.Validation;

/// <summary>
/// Options for validation of train station input.
/// </summary>
public interface ITrainValidatorOptions
{
    bool ValidateAsciiInput { get; }
    int MaxLength { get; }
}