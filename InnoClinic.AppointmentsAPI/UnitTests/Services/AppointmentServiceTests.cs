using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.Services;

public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AppointmentService _service;

    public AppointmentServiceTests()
    {
        _mockRepository = new Mock<IAppointmentRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new AppointmentService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsyncWithValidDtoReturnsAppointment()
    {
        var createDto = AppointmentTestCases.ValidCreateAppointmentDto;
        var appointment = AppointmentTestCases.ValidAppointment;
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;

        _mockMapper.Setup(m => m.Map<Appointment>(createDto)).Returns(appointment);
        _mockRepository.Setup(r => r.CreateAsync(appointment, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockMapper.Setup(m => m.Map<AppointmentDto>(appointment)).Returns(expectedDto);

        var result = await _service.CreateAsync(createDto);

        result.Should().BeEquivalentTo(expectedDto);
    }


    [Fact]
    public async Task GetByIdAsyncWithExistingIdReturnsAppointment()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;

        _mockRepository.Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockMapper.Setup(m => m.Map<AppointmentDto>(appointment)).Returns(expectedDto);

        var result = await _service.GetByIdAsync(appointment.Id);

        result.Should().BeEquivalentTo(expectedDto);
    }


    [Fact]
    public async Task GetAllAsyncReturnsAllAppointments()
    {
        var appointments = new List<Appointment> { AppointmentTestCases.ValidAppointment };
        var expectedDtos = new List<AppointmentDto> { AppointmentTestCases.ValidAppointmentDto };

        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(appointments);
        _mockMapper.Setup(m => m.Map<IReadOnlyCollection<AppointmentDto>>(appointments)).Returns(expectedDtos);

        var result = await _service.GetAllAsync();

        result.Should().BeEquivalentTo(expectedDtos);
    }

    [Fact]
    public async Task UpdateAsyncWithValidDtoReturnsUpdatedAppointment()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        var updateDto = AppointmentTestCases.ValidUpdateAppointmentDto;
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;

        expectedDto.IsAproved = true;
        expectedDto.Date = updateDto.Date;

        _mockRepository.Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockRepository.Setup(r => r.UpdateAsync(appointment, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockMapper.Setup(m => m.Map<AppointmentDto>(appointment)).Returns(expectedDto);

        var result = await _service.UpdateAsync(appointment.Id, updateDto);

        result.Should().BeEquivalentTo(expectedDto);
        _mockMapper.Verify(m => m.Map(updateDto, appointment), Times.Once);
    }

    [Fact]
    public async Task DeleteAsyncWithExistingIdDeletesAppointment()
    {
        var appointment = AppointmentTestCases.ValidAppointment;

        _mockRepository.Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockRepository.Setup(r => r.DeleteAsync(appointment, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.DeleteAsync(appointment.Id);

        _mockRepository.Verify(r => r.DeleteAsync(appointment, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ApproveAppointmentAsyncWithExistingIdUpdatesApprovalStatus()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        var isApproved = true;

        _mockRepository.Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockRepository.Setup(r => r.UpdateAsync(appointment, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);

        await _service.ApproveAppointmentAsync(appointment.Id, isApproved);

        appointment.IsAproved.Should().Be(isApproved);
        _mockRepository.Verify(r => r.UpdateAsync(appointment, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAppointmentByIdWithDependenciesAsyncWithExistingIdReturnsAppointment()
    {
        var appointment = AppointmentTestCases.ValidAppointment;
        var expectedDto = AppointmentTestCases.ValidAppointmentDto;

        _mockRepository.Setup(r => r.GetByIdWithDetailsAsync(appointment.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointment);
        _mockMapper.Setup(m => m.Map<AppointmentDto>(appointment)).Returns(expectedDto);

        var result = await _service.GetAppointmentByIdWithDependenciesAsync(appointment.Id);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetAppointmentsWithDependenciesAsyncReturnsAllAppointments()
    {
        var appointments = new List<Appointment> { AppointmentTestCases.ValidAppointment };
        var expectedDtos = new List<AppointmentDto> { AppointmentTestCases.ValidAppointmentDto };

        _mockRepository.Setup(r => r.GetAllWithDetailsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(appointments);
        _mockMapper.Setup(m => m.Map<IReadOnlyCollection<AppointmentDto>>(appointments)).Returns(expectedDtos);

        var result = await _service.GetAppointmentsWithDependenciesAsync();

        result.Should().BeEquivalentTo(expectedDtos);
    }
}