using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IPenaltyService
    {
        Task<IEnumerable<PenaltyDTO>> GetByUserIdAsync(int userId);
        bool AllowBills(int accounterId, int billId);
        //        Рассчитать штрафы

        //Получить список штрафов пользователя
    }

}
