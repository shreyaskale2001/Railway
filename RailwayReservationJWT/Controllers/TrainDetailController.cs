using RailwayReservationJWT.Data;
using RailwayReservationJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RailwayReservationJWT.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]

    public class TrainDetailController : Controller
    {
        private readonly RailwayContext context;
        public TrainDetailController(RailwayContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainDetail>>> GetTrainDetail()
        {
            if (context.trainDetails == null)
            {
                return NotFound();
            }
            return await context.trainDetails.ToListAsync();
        }
        [HttpGet("id")]
        public async Task<ActionResult<TrainDetail>> GetTrainDetail(int id )
        {
            if (context.trainDetails == null)
            {
                return NotFound();
            }
            var train = await context.trainDetails.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            return train;
        }
        [HttpPost]
        public async Task<ActionResult<TrainDetail>> PostTrainDetail(TrainDetail train
            )
        {
            context.trainDetails.Add(train);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrainDetail), new { id = train.TrainNo }, train);
        }
        [HttpPut]
        public async Task<IActionResult> PutflightDetail(int id, TrainDetail flight)
        {
            if (id != flight.TrainNo)
            {
                return BadRequest();
            }
            context.Entry(flight).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainDetail(int id)
        {
            if (context.trainDetails == null)
            {
                return NotFound();
            }
            var train = await context.trainDetails.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            context.trainDetails.Remove(train);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}