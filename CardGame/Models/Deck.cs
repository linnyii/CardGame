namespace CardGame.Models;

public abstract class Deck<T> where T : class
{
    protected List<T> Cards { get; set; } = [];
    private Random Random { get; } = new();

    public abstract void InitializeDeck();

    public void Shuffle()
    {
        ShuffleAlgorithm();
    }

    public T? DrawCard()
    {
        if (Cards.Count == 0)
        {
            return null;
        }

        var card = Cards[0];
        Cards.RemoveAt(0);
        return card;
    }

    private void ShuffleAlgorithm()
    {
        for (var currentCardIndex = Cards.Count - 1; currentCardIndex > 0; currentCardIndex--)
        {
            var nextCardIndex = Random.Next(currentCardIndex + 1);
            (Cards[currentCardIndex], Cards[nextCardIndex]) = (Cards[nextCardIndex], Cards[currentCardIndex]);
        }
    }
}