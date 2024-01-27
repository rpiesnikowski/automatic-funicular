namespace AbaxService.Validation;


public class SomeDummyValidatorOptions : ITrainValidatorOptions
{
    public bool ValidateAsciiInput { get; } = true;
    public int MaxLength { get; } = 50;
}