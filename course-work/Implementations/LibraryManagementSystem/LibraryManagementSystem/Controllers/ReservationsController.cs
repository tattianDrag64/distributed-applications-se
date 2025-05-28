using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Implementations;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationService _reservationService;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IReservationService reservationService, IReservationRepository reservationRepositpry, IUnitOfWork unitOfWork)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepositpry;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetSortedReservations()
        {
            var reservations = await _reservationRepository.FindAsync(r => r.User != null && r.BookCopy != null);
            var sortedReservations = reservations.OrderBy(r => r.DueDate).ToList();

            if (!sortedReservations.Any())
            {
                return NotFound("No reservations found.");
            }

            return Ok(sortedReservations);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetLoansById(int id)
        {
            var bookUserHas = _reservationService.GetReservationsById(id);

            if (bookUserHas == null)
            {
                return NotFound("There is no resrtvations with that ID");
            }
            return Ok(bookUserHas);
        }

        [HttpPost("{productId}")]
        public async Task<ActionResult<string>> Create([FromRoute] int bookId, [FromBody] int userId)
        {
            Reservation reservation = await _reservationService.CreateReservation(bookId, userId);

            if (reservation != null)
            {
                await _reservationRepository.AddAsync(reservation);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Book exists");
            }
            else
            {
                return NotFound("Error!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id); 

            if (reservation == null)
            {
                return BadRequest("There is no reservation with that ID");
            }
            else
            {
                _reservationRepository.Remove(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
            return Ok("Reservation has been deleted");
        }

        [HttpPut]
        public async Task<IActionResult> ReturnLoan([FromBody] int productCopyId)
        {
            var response = await _reservationService.ReturnReservation(productCopyId);

            if (response == true)
            {
                return Ok("Loan has been updated");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("TopProducts")]
        public async Task<ActionResult<List<Book>>> GetTopBooks()
        {
            var result = await _reservationService.GetTopBooks();
            return Ok(result);
        }
    }
}
