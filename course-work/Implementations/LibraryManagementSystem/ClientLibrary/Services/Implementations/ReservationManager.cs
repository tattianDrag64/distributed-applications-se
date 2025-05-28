using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{

    public class ReservationManager : IReservationManager
    {
        private readonly HttpClient _httpClient;
        public ReservationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var reservations = await _httpClient.GetFromJsonAsync<List<Reservation>>("api/reservations");
            if (reservations == null)
            {
                return null;
            }
            return reservations;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reservation = await _httpClient.GetFromJsonAsync<Reservation>($"api/reservations/{id}");
            if (reservation == null)
            {
                return null;
            }
            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByUserId(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Reservation>>($"api/reservationsByUser/{userId}");
        }

        public async Task<string> AddReservation(int productId, int userId)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/reservations/{productId}", userId);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task UpdateReservation(Reservation reservationToUpdate)
        {
            await _httpClient.PutAsJsonAsync($"api/reservations/{reservationToUpdate.Id}", reservationToUpdate);
        }

        public async Task<bool> ReturnReservationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/reservations/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Book>> GetTopBooks()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Book>>("api/Reservations/TopBooks");
            return result;
        }
    }
}
