using Application.DTOs.KYCDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using Infrastructure.GenericRepo;
using Infrastructure.UnitOfWork;
namespace Application.Services
{
    public class KYCService : IKYCService
    {
        private readonly IUnit _unit;
        private readonly IGenericRepository<KYC, string> _kycRepository;
        private readonly IGenericRepository<User, string> _userRepository;
        private readonly IMapper _mapper;

        public KYCService(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _kycRepository = _unit.GetRepository<KYC, string>();
            _userRepository = _unit.GetRepository<User, string>(); // Get User Repository
            _mapper = mapper;
        }

        public async Task<int> Create(KYCDTO kycRegistrationDto)
        {
            // Check if User Exists
            var userExists = await _userRepository.Get(kycRegistrationDto.UserID);
            if (userExists == null)
            {
                throw new Exception("User not found. Only registered users can apply for KYC.");
            }

            // Check if User Already Has KYC
            var existingKYC = await _kycRepository.Get("UserId", kycRegistrationDto.UserID);
            if (existingKYC != null)
            {
                throw new Exception("User has already applied for KYC.");
            }

            // Convert DTO to Entity
            var kyc = _mapper.Map<KYC>(kycRegistrationDto);
            kyc.Id = Guid.NewGuid().ToString();
            kyc.Status = BankStatus.PENDING.ToString();
            kyc.CreatedOn = DateTime.UtcNow;
            kyc.UserID = kycRegistrationDto.UserID;
            kyc.Name = userExists.Name;
            return await _kycRepository.Add(kyc);
        }

        public async Task<KYC> GetById(string id)
        {
            var kyc = await _kycRepository.Get(id);
            if (kyc == null)
            {
                throw new KeyNotFoundException("KYC record not found.");
            }

            if (Enum.TryParse<BankStatus>(kyc.Status, true, out var status))
            {
                kyc.Status = status.ToString();
            }

            return kyc;
        }



        public async Task<IEnumerable<KYC>> GetAll()
        {
            return await _kycRepository.GetAll("");
        }

        public async Task<int> CountAll()
        {
            return await _kycRepository.Count();
        }

        public async Task<int> Update(KYCDTO kycRegistrationDto)
        {
            var existingKYC = await _kycRepository.Get(kycRegistrationDto.UserID);
            if (existingKYC == null)
            {
                throw new Exception("KYC record not found.");
            }

            _mapper.Map(kycRegistrationDto, existingKYC);

            return await _kycRepository.Update(existingKYC);
        }
        public async Task<int> UpdateStatus(KYCStatusDTO kycStatusDTO)
        {
            // Change "KYC ID" to "Id" to match the primary key
            var existingKYC = await _kycRepository.Get(kycStatusDTO.Id);
            if (existingKYC == null)
            {
                throw new KeyNotFoundException("KYC record not found.");
            }

            existingKYC.Status = kycStatusDTO.Status.ToString();
            existingKYC.LastModifiedBy = Role.Admin.ToString();
            existingKYC.LastModifiedOn = DateTime.UtcNow;

            return await _kycRepository.Update(existingKYC);
        }

        public async Task<bool> SoftDelete(string id)
        {
            var existingKYC = await _kycRepository.Get(id);
            if (existingKYC == null)
            {
                throw new Exception("KYC record not found.");
            }

            return await _kycRepository.SoftDelete(existingKYC) > 0;
        }
    }
}
