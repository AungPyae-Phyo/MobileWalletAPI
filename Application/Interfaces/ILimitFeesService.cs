﻿
using Application.DTOs.LimitFeesDTO;
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Interfaces
{
    public interface ILimitFeesService
    {
        Task<int> Create(LimitFeesDTO limitFeesDto);
        Task<int> CountAll();
        Task<IEnumerable<LimitFees>> GetAll();
        Task<int> Update(string id, LimitFeesDTO limitFeesDto);
        Task<bool> Delete(string id);

    }
}
