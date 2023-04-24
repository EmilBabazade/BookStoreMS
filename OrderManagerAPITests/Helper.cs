using System.Text.Json;

namespace OrderManagerAPITests;
internal static class Helper
{
    internal static void AreEqualByJson(object expected, object actual)
    {
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }
}
