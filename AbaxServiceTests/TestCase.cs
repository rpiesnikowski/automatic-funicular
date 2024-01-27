namespace AbaxServiceTests;

public class TestCase
{
    private string Title;

    public TestCase(string title)
    {
        Title = title;
    }

    public override string ToString()
    {
        return $"{nameof(Title)}: {Title}";
    }
}