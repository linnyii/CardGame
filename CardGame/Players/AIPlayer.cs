namespace CardGame.Players;

public class AiPlayer<TCard>(string name, int? maxCards = null) : Player<TCard>(name, maxCards);

