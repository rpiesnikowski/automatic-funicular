namespace AbaxService.Validation;

public interface ITrainInputValidator
{
    TrainValidationResult Validate(string input);
}