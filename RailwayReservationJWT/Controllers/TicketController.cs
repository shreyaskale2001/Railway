
using RailwayReservationJWT.Data;
using RailwayReservationJWT.Models;
using RailwayReservationJWT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RailwayReservationJWT.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]

    public class BookingController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RailwayContext context;
        public BookingController(UserManager<IdentityUser> userManager, RailwayContext context)
        {
            this._userManager = userManager;
            this.context = context;
        }
        [HttpPost]
        //[Route("Booking")]
        public async Task<IActionResult> Booking([FromForm] TicketData ticketData)
        {
            string userId = "2c238ea5-42f2-4ebb-9ea5-e6a9b8df6164";

            if (userId != null)
            {
                int TId = ticketData.TrainNo;
                TrainDetail trainDetail = context.trainDetails.FirstOrDefault(id => id.TrainNo == TId);
                Ticket ticket = new Ticket();
                ticket.UserName = ticketData.UserName;
                ticket.Age = ticketData.Age;
                ticket.Gender = ticketData.Gender;
                ticket.UserId = userId;
                ticket.TrainNo = TId;

                if (ticketData.TicketType == "SL" && trainDetail.SeatCount_Slepper > 0)
                {
                    ticket.SeatNo = "SL" + (trainDetail.SeatCount_Slepper - trainDetail.SeatCount_Slepper + 1);
                    trainDetail.SeatCount_Slepper -= 1;
                }
                else if (ticketData.TicketType == "AC1" && trainDetail.SeatCount_AC1tire > 0)
                {
                    ticket.SeatNo = "AC1" + (trainDetail.SeatCount_AC1tire - trainDetail.SeatCount_AC1tire + 1);
                    trainDetail.SeatCount_AC1tire -= 1;
                }
                else if (ticketData.TicketType == "AC2" && trainDetail.SeatCount_AC2tire > 0)
                {
                    ticket.SeatNo = "AC2" + (trainDetail.SeatCount_AC2tire - trainDetail.SeatCount_AC2tire + 1);
                    trainDetail.SeatCount_AC2tire -= 1;
                }
                else if (ticketData.TicketType == "AC3" && trainDetail.SeatCount_AC3tire > 0)
                {
                    ticket.SeatNo = "AC3" + (trainDetail.SeatCount_AC3tire - trainDetail.SeatCount_AC3tire + 1);
                    trainDetail.SeatCount_AC3tire -= 1;
                }
                else
                {
                    ticket.SeatNo = "G" + (trainDetail.SeatCount_SecoundSetting - trainDetail.SeatCount_SecoundSetting + 1);
                    trainDetail.SeatCount_SecoundSetting -= 1;
                }

                context.tickets.Add(ticket);
                context.SaveChanges();
                if (ticket.TicketNo != 0)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Booking Successfull",
                        BookingId = ticket.TicketNo.ToString(),
                        SeatNo = ticket.SeatNo
                    });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "User not signed in", Message = "Please Signin!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Unable to book the ticket", Message = "Please try again!" });
        }
    }
}