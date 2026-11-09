using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalManagementSystem.Application.Features.Customer.Command.BookVehicle;
using RentalManagementSystem.Application.Features.Customer.Command.CancelBooking;
using RentalManagementSystem.Application.Features.Customer.Query.GetAllExtraServices;
using RentalManagementSystem.Application.Features.Customer.Query.GetPagedVehicle;
using RentalManagementSystem.Application.Features.Customer.Query.GetRentalCompany;
using RentalManagementSystem.Application.Features.Customer.Query.GetVehicleCategory;
using RentalManagementSystem.Application.Features.Customer.Query.MyPagedRentals;

namespace RentalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController(IMediator mediator) : ControllerBase
    {

        [HttpGet("GetAvailableVehicle")]
        public async Task<IActionResult> GetPagedAvailableVehicle([FromQuery]GetAvailablePagedVehicleQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetMyRentals")]
        public async Task<IActionResult> GetMyRentals([FromQuery]MyPagedRentalsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("BookVehicle")]
        public async Task<IActionResult> BookVehicle(BookVehicleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("CancelBooking")]
        public async Task<IActionResult> CancelBooking( CancelBookingCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetRentalCompany")]
        public async Task<IActionResult> GetRentalCompany([FromQuery]GetRentalCompanyQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetVehicleCategory")]
        public async Task<IActionResult> GetVehicleCategory([FromQuery]GetVehicleCategoryQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetExtraServices")]
        public async Task<IActionResult> GetExtraServices([FromQuery] GetAllExtraServicesQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        
    }
}
