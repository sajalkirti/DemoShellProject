using DemoShellProject.Models;
using DemoShellProject.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace DemoShellProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly CardService _service;
        private readonly ILogger<CardController> _logger;

        public CardController(CardService service, ILogger<CardController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("issue")]
        public IActionResult Issue([FromBody] Card card)
        {
            _service.IssueCard(card);
            _logger.LogInformation("Card {CardId} issued to user {UserId}", card.CardId, card.UserId);
            return Ok("Card issued");
        }

        [HttpPost("transaction")]
        public IActionResult Transaction([FromBody] Card tx)
        {
            try
            {
                _service.RecordTransaction(tx);
                _logger.LogInformation("Transaction recorded for card {CardId}, amount {Amount}", tx.CardId, tx.Balance);
                return Ok("Transaction success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction failed for card {CardId}", tx.CardId);
                return BadRequest("Transaction failed");
            }
        }
    }
}
