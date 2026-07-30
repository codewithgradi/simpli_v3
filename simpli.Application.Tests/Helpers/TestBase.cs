using AutoFixture;

namespace simpli.Application.Tests.Helpers;
public class TestBase
{
    private readonly Fixture _fixture ;
    public TestBase()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}