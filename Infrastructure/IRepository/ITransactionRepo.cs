using Application.DTOs.TransactionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface ITransactionRepo
    {
        Task<List<TransactionResponseDTO>> GetAllForAdmin();
    }
}
