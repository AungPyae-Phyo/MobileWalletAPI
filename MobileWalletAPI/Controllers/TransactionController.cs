using Application.DTOs.TransactionDTO;
using Application.Interfaces;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MobileWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IWalletService _walletService;
        private readonly ITransactionService _service;

        public TransactionController(ITransactionRepo transactionRepo, IWalletService walletService, ITransactionService service)
        {
            _transactionRepo = transactionRepo;
            _walletService = walletService;
            _service = service;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Create([FromBody] TransactionRequestDTO dto)
        {
            try
            {
                var transactionId = await _service.CreateTransaction(dto);

                return Ok(new
                {
                    message = "Transaction created successfully",
                    status = "success",
                    data = new { transactionId }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Transaction failed",
                    status = "error",
                    errors = new List<object> {
                        new { field = "Transaction", message = ex.Message }
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetManuallyMappedTransactions()
        {
            var entities = await _transactionRepo.GetAllForAdmin();
            var result = new List<TransactionResponseDTO>();

            foreach (var transaction in entities)
            {
                var dto = new TransactionResponseDTO
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    TransactionType = transaction.TransactionType,
                    LastModifiedBy = transaction.LastModifiedBy,
                    LastModifiedOn = transaction.LastModifiedOn,
                    SenderName = transaction.SenderName,
                    ReceiverAccountNumber = transaction.ReceiverAccountNumber,
                };

                result.Add(dto);
            }

            return Ok(new
            {
                message = "Transactions fetched successfully",
                status = "success",
                count = result.Count,
                data = result
            });
        }
    }
}
