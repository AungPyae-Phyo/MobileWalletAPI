using Application.DTOs.LimitFeesDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Infrastructure.GenericRepo;
using Infrastructure.UnitOfWork;

namespace Application.Services
{
    public class LimitFeesService : ILimitFeesService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<LimitFees, string> _limitFees;
        private readonly IMapper _mapper;

        public LimitFeesService(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _limitFees = _unit.GetRepository<LimitFees, string>();
            _mapper = mapper;
        }

        public async Task<int> Update(string id, LimitFeesDTO limitFeesDto)
        {
            var entity = await _limitFees.Get(id);
            if (entity == null)
                throw new Exception("LimitFees not found");

            _mapper.Map(limitFeesDto, entity); 
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.LastModifiedBy = "Admin";

            return await _limitFees.Update(entity);
        }


        public async Task<int> Create(LimitFeesDTO limitFeesDto)
        {
            var entity = _mapper.Map<LimitFees>(limitFeesDto);
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedOn = DateTime.UtcNow;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.CreatedBy = "admin";
            entity.LastModifiedBy = "admin";

            return await _limitFees.Add(entity);
        }

        public async Task<int> CountAll()
        {
            return await _limitFees.Count();
        }

        public async Task<IEnumerable<LimitFees>> GetAll()
        {
            return await _limitFees.GetAll("");
        }
    }
}
