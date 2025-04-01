using Application.DTOs.KYCDTO;
using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IKYCService
    {
        Task<int> Create(KYCDTO kycRegistrationDto);
        Task<IEnumerable<KYC>> GetAll();
        Task<int> UpdateStatus(KYCStatusDTO kycStatusDTO);
        Task<KYC> GetById(string id);
        Task<int> Update(KYCDTO kycRegistrationDto);
        Task<bool> SoftDelete(string id);
        Task<int> CountAll();
        
    }
}
