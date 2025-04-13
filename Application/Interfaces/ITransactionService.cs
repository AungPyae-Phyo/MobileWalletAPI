using Application.DTOs.TransactionDTO;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<string> CreateTransaction(TransactionRequestDTO dto);
        Task<IEnumerable<Transaction>> GetAll();
        Task<int> CountAll();
    }
}
