namespace CardGame.Models;

public interface IHandCards
{
    int Count { get; }
    bool HasCards();
}

