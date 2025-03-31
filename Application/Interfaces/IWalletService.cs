using Application.DTOs.WalletDTO;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWalletService
    {
        Task<int> Create(WalletDTO walletDto);
        Task<int> UpdateBalance(string userId, decimal amount);
        Task<WalletDTO> Get(string userId);
        Task<IEnumerable<Wallet>> GetAll();
        Task<int> Update(WalletDTO walletDto);
        Task<bool> SoftDelete(string userId);
        Task<int> CountAll();


    }
}
