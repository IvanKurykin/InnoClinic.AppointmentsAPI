using BLL.Dto;
using BLL.Helpers.Constants;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentResultController(IAppointmentResultService appointmentResultService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<AppointmentResultDto>> CreateAppointmentResultAsync([FromBody] CreateAppointmentResultDto dto, CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.CreateAsync(dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AppointmentResultDto>> UpdateAppointmentResultAsync([FromBody] UpdateAppointmentResultDto dto, [FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.UpdateAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAppointmentResultAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await appointmentResultService.DeleteAsync(id, cancellationToken);

        return Ok(ControllerMessages.AppointmentCanceledSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResultDto?>> GetAppointmentResultByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.GetByIdAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AppointmentResultDto>>> GetAppointmentResultsAsync(CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.GetAllAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("with-appointment/{id}")]
    public async Task<ActionResult<AppointmentResultDto?>> GetAppointmentResultWithAppointmentByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.GetResultWithAppointmentAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("by-doctor-id/{id}")]
    public async Task<ActionResult<IReadOnlyCollection<AppointmentResultDto>>> GetAppointmentResultsByDoctorIdAsync(Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentResultService.GetResultsByDoctorIdAsync(id, cancellationToken));
}