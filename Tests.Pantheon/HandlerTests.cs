using Tests.Pantheon.Base;
using Apps.Pantheon.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Tests.Pantheon;

[TestClass]
public class HandlerTests : TestBase
{
    [TestMethod]
    public async Task Dynamic_handler_works()
    {
        var handler = new DynamicHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }

        Assert.IsTrue(result.Count() > 0);
    }
}
