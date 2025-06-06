using API.Controllers;
using BLL.Dto;
using BLL.Helpers.Constants;
using BLL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.Controllers;

public class AppointmentResultControllerTests
{
    private readonly Mock<IAppointmentResultService> _appointmentResultServiceMock;
    private readonly AppointmentResultController _controller;

    public AppointmentResultControllerTests()
    {
        _appointmentResultServiceMock = new Mock<IAppointmentResultService>();
        _controller = new AppointmentResultController(_appointmentResultServiceMock.Object);
    }

    [Fact]
    public async Task CreateAppointmentResultAsyncReturnsOk()
    {
        var createDto = AppointmentResultTestCases.ValidCreateAppointmentResultDto;
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;
        _appointmentResultServiceMock.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.CreateAppointmentResultAsync(createDto, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task UpdateAppointmentResultAsyncReturnsOk()
    {
        var updateDto = AppointmentResultTestCases.ValidUpdateAppointmentResultDto;
        var id = Guid.NewGuid();
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;
        _appointmentResultServiceMock.Setup(s => s.UpdateAsync(id, updateDto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.UpdateAppointmentResultAsync(updateDto, id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task DeleteAppointmentResultAsyncReturnsOk()
    {
        var id = Guid.NewGuid();
        _appointmentResultServiceMock.Setup(s => s.DeleteAsync(id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.DeleteAppointmentResultAsync(id, CancellationToken.None);

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(ControllerMessages.AppointmentCanceledSuccessfullyMessage);
    }

    [Fact]
    public async Task GetAppointmentResultByIdAsyncReturnsDto()
    {
        var id = Guid.NewGuid();
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;
        _appointmentResultServiceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.GetAppointmentResultByIdAsync(id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task GetAppointmentResultsAsyncReturnsList()
    {
        var expectedList = new List<AppointmentResultDto> { AppointmentResultTestCases.ValidAppointmentResultDto };
        _appointmentResultServiceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedList);

        var result = await _controller.GetAppointmentResultsAsync(CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedList);
    }

    [Fact]
    public async Task GetAppointmentResultWithAppointmentByIdAsyncReturnsDto()
    {
        var id = Guid.NewGuid();
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;
        _appointmentResultServiceMock.Setup(s => s.GetResultWithAppointmentAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.GetAppointmentResultWithAppointmentByIdAsync(id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task GetAppointmentResultsByDoctorIdAsyncReturnsList()
    {
        var doctorId = Guid.NewGuid();
        var expectedList = new List<AppointmentResultDto> { AppointmentResultTestCases.ValidAppointmentResultDto };
        _appointmentResultServiceMock.Setup(s => s.GetResultsByDoctorIdAsync(doctorId, It.IsAny<CancellationToken>())).ReturnsAsync(expectedList);

        var result = await _controller.GetAppointmentResultsByDoctorIdAsync(doctorId, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedList);
    }
}