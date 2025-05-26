using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IReviewService
    {
        //Task<IEnumerable<RatingDetailsDTO>> GetAllRatingsOfAUser(int userId);
        Task<IEnumerable<ReviewDTO>> GetByBookIdAsync(int bookId);
    }
}
