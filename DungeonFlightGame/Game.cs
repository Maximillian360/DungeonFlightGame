namespace DungeonFlightGame;

public class Game
{
    public enum GameDifficulty
    {
        Easy,
        Normal,
        Hard
    }

    public int DifficultyModifier {get; private set;}
}