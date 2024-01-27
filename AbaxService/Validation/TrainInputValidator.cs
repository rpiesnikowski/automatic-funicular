namespace AbaxService.Validation;

public class TrainInputValidator : ITrainInputValidator
{
    private readonly ITrainValidatorOptions options;

    public TrainInputValidator(ITrainValidatorOptions options)
    {
        this.options = options;
    }

    public TrainValidationResult Validate(string input)
    {
        // the best validation should be having all charcters allowed.
        // otherwise it can be validated with some regex or even checking if it is ascii input
        
        if (string.IsNullOrWhiteSpace(input))
        {
            var message = "Empty input.";
            return new TrainValidationResult(false, message);
        }
        
        if (options.ValidateAsciiInput && !input.All(char.IsAscii))
        {
            var message = "Non ASCII characters found.";
            return new TrainValidationResult(false, message);
        }

        if (input.Length > options.MaxLength)
        {
            var message = "Maximum length reached.";
            return new TrainValidationResult(false, message);
        }
        
        return new TrainValidationResult(true);
    }
}