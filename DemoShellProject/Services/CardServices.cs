using DemoShellProject.Models;
using System.Transactions;

namespace DemoShellProject.Services
{
    public class CardService
    {
        private readonly List<Card> _cards = new();

        public void IssueCard(Card card)
        {
            _cards.Add(card);
        }

        public void RecordTransaction(Card tx)
        {
            var card = _cards.FirstOrDefault(c => c.CardId == tx.CardId);
            if (card == null) throw new Exception("Card not found");
            if (card.Balance < tx.Balance) throw new Exception("Insufficient balance");
            card.Balance -= tx.Balance;
        }
    }
}
