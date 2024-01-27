using AbaxService;
using AbaxService.Validation;

namespace AbaxServiceTests;

public class TrainInputValidatorTests
{
    private TrainInputValidator validator;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        validator = new TrainInputValidator(new SomeDummyValidatorOptions());
    }

    public static List<ValidationTestCase> cases = new()
    {
        new ValidationTestCase("Abbey Wood") {Input = "Abbey Wood", IsValid = true},
        new ValidationTestCase("Abergele & Pensarn") {Input = "Abergele & Pensarn", IsValid = true},
        new ValidationTestCase("Acle") {Input = "Acle", IsValid = true},
        new ValidationTestCase("Acton Bridge (Cheshire)") {Input = "Acton Bridge (Cheshire)", IsValid = true},
        new ValidationTestCase("Ascott-under-Wychwood") {Input = "Ascott-under-Wychwood", IsValid = true},
        new ValidationTestCase("Heathrow Airport Terminals 1, 2 and 3") {Input = "Heathrow Airport Terminals 1, 2 and 3", IsValid = true}
    };
    
    public static List<ValidationTestCase> negatives = new()
    {
        new ValidationTestCase("Null input") {Input = null, IsValid = false, ValidationMessage = "Empty input."},
        new ValidationTestCase("Empty input") {Input = string.Empty, IsValid = false, ValidationMessage = "Empty input."},
        new ValidationTestCase("Unicode") {Input = "🎄", IsValid = false, ValidationMessage = "Non ASCII characters found."},
        new ValidationTestCase("Input greater than 50") {Input = "Heathrow Airport Terminals 1, 2 and 3, Heathrow Airport Terminals 1, 2 and 3, Heathrow Airport Terminals 1, 2 and 3", IsValid = false, ValidationMessage = "Maximum length reached."}
    };
    
    [TestCaseSource(nameof(cases))]
    public void TrainInputValidator_Validate(ValidationTestCase tc)
    {
        var result = validator.Validate(tc.Input);
        Assert.That(result.IsValid, Is.EqualTo(tc.IsValid));
        Assert.Pass();
    }
    
    [TestCaseSource(nameof(negatives))]
    public void TrainInputValidator_Negative_Validate(ValidationTestCase tc)
    {
        var result = validator.Validate(tc.Input);
        Assert.That(result.IsValid, Is.EqualTo(tc.IsValid));
        Assert.That(result.ValidationMessage, Is.EqualTo(tc.ValidationMessage));
        Assert.Pass();
    }
    public class ValidationTestCase : TestCase
    {
        public string Input { get; set; }
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }

        public ValidationTestCase(string title) : base(title)
        {
        }
    }
    
}