using API.Controllers;
using BLL.Dto;
using BLL.Helpers.Constants;
using BLL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.Controllers;

public class AppointmentControllerTests
{
    private readonly Mock<IAppointmentService> _appointmentServiceMock;
    private readonly AppointmentController _controller;

    public AppointmentControllerTests()
    {
        _appointmentServiceMock = new Mock<IAppointmentService>();
        _controller = new AppointmentController(_appointmentServiceMock.Object);
    }

    [Fact]
    public async Task CreateAppointmentAsyncReturnsOk()
    {
        var createDto = AppointmentTestCases.ValidCreateAppointmentDto;
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;
        _appointmentServiceMock.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.CreateAppointmentAsync(createDto, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task UpdateAppointmentAsyncReturnsOk()
    {
        var updateDto = AppointmentTestCases.ValidUpdateAppointmentDto;
        var id = Guid.NewGuid();
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;
        _appointmentServiceMock.Setup(s => s.UpdateAsync(id, updateDto, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.UpdateAppointmentAsync(updateDto, id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task ApproveAppointmentAsyncReturnsOk()
    {
        var id = Guid.NewGuid();
        var isApproved = true;
        _appointmentServiceMock.Setup(s => s.ApproveAppointmentAsync(id, isApproved, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.ApproveAppointmentAsync(id, isApproved, CancellationToken.None);

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(ControllerMessages.AppointmentApprovedSuccessfullyMessage);
    }

    [Fact]
    public async Task CancelAppointmentAsyncReturnsOk()
    {
        var id = Guid.NewGuid();
        _appointmentServiceMock.Setup(s => s.DeleteAsync(id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.CancelAppointmentAsync(id, CancellationToken.None);

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(ControllerMessages.AppointmentCanceledSuccessfullyMessage);
    }

    [Fact]
    public async Task GetAppointmentByIdAsyncReturnsDto()
    {
        var id = Guid.NewGuid();
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;
        _appointmentServiceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.GetAppointmentByIdAsync(id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }

    [Fact]
    public async Task GetAppointmentsAsyncReturnsList()
    {
        var expectedList = new List<AppointmentDto> { AppointmentTestCases.ValidAppointmentDto };
        _appointmentServiceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedList);

        var result = await _controller.GetAppointmentsAsync(CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedList);
    }

    [Fact]
    public async Task GetServiceCategoriesWithDependenciesAsyncReturnsList()
    {
        var expectedList = new List<AppointmentDto> { AppointmentTestCases.ValidAppointmentDto };
        _appointmentServiceMock.Setup(s => s.GetAppointmentsWithDependenciesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedList);

        var result = await _controller.GetServiceCategoriesWithDependenciesAsync(CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedList);
    }

    [Fact]
    public async Task GetAppointmentByIdWithDetailsAsyncReturnsDto()
    {
        var id = Guid.NewGuid();
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;
        _appointmentServiceMock.Setup(s => s.GetAppointmentByIdWithDependenciesAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedDto);

        var result = await _controller.GetAppointmentByIdWithDetailsAsync(id, CancellationToken.None);

        result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(expectedDto);
    }
}