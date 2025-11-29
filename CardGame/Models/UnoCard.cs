using CardGame.Enums;

namespace CardGame.Models;

public class UnoCard(Color color, UnoCardValue value)
{
    private Color Color { get; } = color;
    private UnoCardValue Value { get; } = value;

    public bool CanPlay(UnoCard currentCard)
    {
        return Color == currentCard.Color || Value == currentCard.Value;
    }
}

