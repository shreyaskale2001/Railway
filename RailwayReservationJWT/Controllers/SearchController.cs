using RailwayReservationJWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationJWT.Data;
using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace RailwayReservationJWT.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly RailwayContext context;
        public SearchController(RailwayContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search(DateTime StartDate, string ArrivalLocation, string DestinationLocation)
        {
            DateTime sDate = DateTime.Parse(StartDate.ToString());
            DateTime nDate = sDate.Date;
            DateTime tDate = sDate.AddDays(1).Date;
            List<TrainDetail> res = context.trainDetails.Where(
                        bd => bd.ArrivalLocation == ArrivalLocation && bd.DestinationLocation == DestinationLocation
                        && bd.StartDate >= nDate && bd.StartDate < tDate
                    ).ToList();
            if (res.Count > 0)
            {
                var json = JsonSerializer.Serialize(res);
                return Ok(res);
                
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Train not available", Message = "Sorry, not any train is available according to your requirements!" });


        }
    }
}