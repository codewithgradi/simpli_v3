namespace simpli.Application.Tests;

using AutoFixture;
using Moq;
using simpli.Application.Dtos;
using simpli.Application.Services;
using simpli.Application.Tests.Helpers;
using simpli.Domain;

public class CompanyServiceTests:TestBase
{
    private readonly Mock<ICompanyRepo> _mockRepo=new ();
    private readonly CompanyService _serviceUnderTest;
    private readonly CompanyMappers _companyMappers = new ();
    private readonly Fixture _fixture = new();
    public CompanyServiceTests()
    {
        _serviceUnderTest = new CompanyService(_mockRepo.Object, _companyMappers);
    }

    [Fact]
    public async Task GetCompanyProfile_IfCompanyExists_ReturnsCompanyDto()
    {
        //Arrage
        var entity = _fixture.Create<Company>();
        _mockRepo.Setup(repo=>repo.GetCompanyProfile(entity.Id)).ReturnsAsync(entity);

        //Act
        var result = await _serviceUnderTest.GetCompanyProfile(entity.Id);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        _mockRepo.Verify(repo=>repo.GetCompanyProfile(entity.Id),Times.Once());
        
    } 
    
}