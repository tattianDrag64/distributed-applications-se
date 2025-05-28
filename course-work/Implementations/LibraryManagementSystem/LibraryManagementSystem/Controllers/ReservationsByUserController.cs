using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsByUserController : ControllerBase
    {
        private readonly IReservationsByUserRepository _reservationsByUserRepository;

        public ReservationsByUserController(IReservationsByUserRepository reservationsByUserRepository)
        {
            _reservationsByUserRepository = reservationsByUserRepository;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<List<Reservation>>> GetReservationsByCurrentUser(int userId)
        {
            var reservations = await _reservationsByUserRepository.GetActiveReservationsByUser(userId);

            if (reservations == null || !reservations.Any())
            {
                return NotFound("No reservations found for the current user.");
            }

            return Ok(reservations);
        }
    }
}
