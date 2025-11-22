using CardGame.Enums;

namespace CardGame.Models;

public class UnoCard(Color color, int number)
{
    private Color Color { get; } = color;
    private int Number { get; } = number;

    public bool CanPlay(UnoCard currentCard)
    {
        return Color == currentCard.Color || Number == currentCard.Number;
    }
}

