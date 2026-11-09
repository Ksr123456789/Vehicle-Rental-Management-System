using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalManagementSystem.Application.Features.Admin.command.AddRentalCompany;
using RentalManagementSystem.Application.Features.Admin.command.AddVehicle;
using RentalManagementSystem.Application.Features.Admin.command.DeleteCompany;
using RentalManagementSystem.Application.Features.Admin.command.DeleteVehicle;
using RentalManagementSystem.Application.Features.Admin.command.UpdateBookingStatus;
using RentalManagementSystem.Application.Features.Admin.command.UpdateRentalCompany;
using RentalManagementSystem.Application.Features.Admin.command.UpdateVehicle;
using RentalManagementSystem.Application.Features.Admin.query.GetAllRentalCompany;
using RentalManagementSystem.Application.Features.Admin.query.GetAllVehicleCategory;
using RentalManagementSystem.Application.Features.Admin.query.GetBookingDashBoard;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedBooking;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedRentalCompany;
using RentalManagementSystem.Application.Features.Admin.query.GetPagedVehicle;

namespace RentalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {

        [HttpPost("AddRentalCompany")]
        public async Task<IActionResult> AddRentalCompany(AddRentalCompanyCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetPagedRentalCompany")]
        public async Task<IActionResult> GetPagedRentalCompany([FromQuery]GetPagedRentalCompanyQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAllRentalCompany")]
        public async Task<IActionResult> GetAllRentalCompany([FromQuery]GetAllRentalCompanyQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
      
        [HttpPut("UpdateRentalCompany")]
        public async Task<IActionResult> UpdateRentalCompany(UpdateRentalCompanyCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteRentalCompany")]
        public async Task<IActionResult> DeleteRentalCompany([FromQuery]DeleteCompanyCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle(AddVehicleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetPagedVehicle")]
        public async Task<IActionResult> GetPagedVehicle([FromQuery]GetPagedVehicleQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("GetAllVehicleCategory")]
        public async Task<IActionResult> GetAllVehicleCategory([FromQuery]GetAllVehicleCategoryQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle([FromQuery]DeleteVehicleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle(UpdateVehicleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetPagedBooking")]
        public async Task<IActionResult> GetPagedBooking([FromQuery]GetPagedBookingQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("UpdateBookingStatus")]
        public async Task<IActionResult> UpdateBookingStatus(UpdateBookingStatusCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
       [ HttpGet("GetBookingDashBoard")]
       public async Task<IActionResult> GetBookingDashboard([FromQuery]GetBookingDashBoardQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
        

    }
}
