namespace CardGame.Players;

public class HumanPlayer<TCard>(string name, int? maxCards = null) : Player<TCard>(name, maxCards)
{
}

