namespace CardGame.Models;

public class PokerHandCards
{
    public List<PokerCard> Cards { get; } = [];

    public void DisplayEachCard()
    {
        var cardIndex = 1;
        foreach (var card in Cards)
        {
            Console.WriteLine($"{cardIndex}. {card.Rank}. {card.Suit}");
            cardIndex++;
        }
    }

    public PokerCard ManualChooseACard()
    {
        while (true)
        {
            Console.Write("請選擇 (輸入編號): ");
            if (int.TryParse(Console.ReadLine(), out var choice) && 
                choice >= 1 && choice <= Cards.Count)
            {
                return Cards[choice - 1];
            }
            Console.WriteLine("無效的選擇，請重新輸入。");
        }
    }

    public PokerCard RandomChooseCard()
    {
        return Cards[new Random().Next(Cards.Count)];
    }
}