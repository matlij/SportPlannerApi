using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportPlanner.ModelsDto;
using SportPlanner.Repository.Models;
using SportPlanner.Repository.Profiles;

namespace SportPlanner.Repository.Tests;

[TestClass]
public class SportPlannerProfileTest
{
    private Fixture _fixture = new Fixture();
    private MapperConfiguration _mapperConf;
    private IMapper _mapper;

    public SportPlannerProfileTest()
    {
        var confExpression = new MapperConfigurationExpression();
        confExpression.AddProfile<SportPlannerProfile>();
        _mapperConf = new MapperConfiguration(confExpression);
        _mapper = _mapperConf.CreateMapper();
    }

    [TestMethod]
    public void AssertConfigurationIsValid()
    {
        _mapperConf.AssertConfigurationIsValid();
    }

    [TestMethod]
    public void EventDto_to_Event()
    {
        var eventDto = _fixture.Create<EventDto>();
        var @event = new Event();

        _mapper.Map(eventDto, @event);

        //foreach (var eventUser in eventDto.Users)
        //{
        //    eventUser.EventId.Should().Be(eventDto.Id);
        //}
    }
}
