using System.Globalization;

namespace ImageFiltering.Tests;

[TestClass]
public static class TestInitializer
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
    }
}
