using AutoMapper;
using BLL.Dto;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.Services;

public class AppointmentResultServiceTests
{
    private readonly Mock<IAppointmentResultRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AppointmentResultService _service;

    public AppointmentResultServiceTests()
    {
        _mockRepository = new Mock<IAppointmentResultRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new AppointmentResultService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsyncWithValidDtoReturnsAppointmentResult()
    {
        var createDto = AppointmentResultTestCases.ValidCreateAppointmentResultDto;
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;

        _mockMapper.Setup(m => m.Map<AppointmentResult>(createDto)).Returns(appointmentResult);
        _mockRepository.Setup(r => r.CreateAsync(appointmentResult, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockMapper.Setup(m => m.Map<AppointmentResultDto>(appointmentResult)).Returns(expectedDto);

        var result = await _service.CreateAsync(createDto);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetByIdAsyncWithExistingIdReturnsAppointmentResult()
    {
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;

        _mockRepository.Setup(r => r.GetByIdAsync(appointmentResult.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockMapper.Setup(m => m.Map<AppointmentResultDto>(appointmentResult)).Returns(expectedDto);

        var result = await _service.GetByIdAsync(appointmentResult.Id);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetAllAsyncReturnsAllAppointmentResultDtos()
    {
        var appointmentResults = new List<AppointmentResult> { AppointmentResultTestCases.ValidAppointmentResult };
        var expectedDtos = new List<AppointmentResultDto> { AppointmentResultTestCases.ValidAppointmentResultDto };

        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResults);
        _mockMapper.Setup(m => m.Map<IReadOnlyCollection<AppointmentResultDto>>(appointmentResults)).Returns(expectedDtos);

        var result = await _service.GetAllAsync();

        result.Should().BeEquivalentTo(expectedDtos);
    }

    [Fact]
    public async Task UpdateAsyncWithValidDtoReturnsUpdatedAppointmentResult()
    {
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;
        var updateDto = AppointmentResultTestCases.ValidUpdateAppointmentResultDto;
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;

        expectedDto.Diagnosis = updateDto.Diagnosis;
        expectedDto.Recommendations = updateDto.Recommendations;

        _mockRepository.Setup(r => r.GetByIdAsync(appointmentResult.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockRepository.Setup(r => r.UpdateAsync(appointmentResult, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockMapper.Setup(m => m.Map<AppointmentResultDto>(appointmentResult)).Returns(expectedDto);

        var result = await _service.UpdateAsync(appointmentResult.Id, updateDto);

        result.Should().BeEquivalentTo(expectedDto);
        _mockMapper.Verify(m => m.Map(updateDto, appointmentResult), Times.Once);
    }

    [Fact]
    public async Task DeleteAsyncWithExistingIdDeletesAppointmentResult()
    {
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;

        _mockRepository.Setup(r => r.GetByIdAsync(appointmentResult.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockRepository.Setup(r => r.DeleteAsync(appointmentResult, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.DeleteAsync(appointmentResult.Id);

        _mockRepository.Verify(r => r.DeleteAsync(appointmentResult, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetResultWithAppointmentAsyncWithExistingIdReturnsAppointmentResult()
    {
        var appointmentResult = AppointmentResultTestCases.ValidAppointmentResult;
        var expectedDto = AppointmentResultTestCases.ValidAppointmentResultDto;

        _mockRepository.Setup(r => r.GetByIdAsync(appointmentResult.Id, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResult);
        _mockMapper.Setup(m => m.Map<AppointmentResultDto>(appointmentResult)).Returns(expectedDto);

        var result = await _service.GetResultWithAppointmentAsync(appointmentResult.Id);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetResultsByDoctorIdAsyncReturnsAppointmentResults() 
    { 
        var doctorId = Guid.NewGuid();
        var appointmentResults = new List<AppointmentResult> { AppointmentResultTestCases.ValidAppointmentResult };
        var expectedDtos = new List<AppointmentResultDto> { AppointmentResultTestCases.ValidAppointmentResultDto };

        _mockRepository.Setup(r => r.GetByDoctorIdAsync(doctorId, It.IsAny<CancellationToken>())).ReturnsAsync(appointmentResults);
        _mockMapper.Setup(m => m.Map<IReadOnlyCollection<AppointmentResultDto>>(appointmentResults)).Returns(expectedDtos);

        var result = await _service.GetResultsByDoctorIdAsync(doctorId);

        result.Should().BeEquivalentTo(expectedDtos);
    }
}