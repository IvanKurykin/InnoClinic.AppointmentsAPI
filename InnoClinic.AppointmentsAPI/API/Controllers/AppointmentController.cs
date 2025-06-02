using BLL.Dto;
using BLL.Helpers.Constants;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> CreateAppointmentAsync([FromBody] CreateAppointmentDto dto, CancellationToken cancellationToken) =>
        Ok(await appointmentService.CreateAsync(dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointmentAsync([FromBody] UpdateAppointmentDto dto, [FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentService.UpdateAsync(id, dto, cancellationToken));

    [HttpPut("approve/{id}")]
    public async Task<ActionResult> ApproveAppointmentAsync([FromRoute] Guid id, [FromQuery] bool isApproved, CancellationToken cancellationToken)
    {
        await appointmentService.ApproveAppointmentAsync(id, isApproved, cancellationToken);

        return Ok(ControllerMessages.AppointmentApprovedSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> CancelAppointmentAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await appointmentService.DeleteAsync(id, cancellationToken);

        return Ok(ControllerMessages.AppointmentCanceledSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDto?>> GetAppointmentByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentService.GetByIdAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AppointmentDto>>> GetAppointmentsAsync(CancellationToken cancellationToken) =>
        Ok(await appointmentService.GetAllAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("with-dependencies")]
    public async Task<ActionResult<IReadOnlyCollection<AppointmentDto>>> GetServiceCategoriesWithDependenciesAsync(CancellationToken cancellationToken) =>
        Ok(await appointmentService.GetAppointmentsWithDependenciesAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("with-dependencies/{id}")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentByIdWithDetailsAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await appointmentService.GetAppointmentByIdWithDependenciesAsync(id, cancellationToken));
}