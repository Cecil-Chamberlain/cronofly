using Xunit;

namespace Cronofly.AcceptanceTests
{
    [CollectionDefinition(nameof(Fixture))]
    public class FixtureCollection : ICollectionFixture<Fixture>
    {

    }
}
