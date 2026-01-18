namespace DungeonFlightGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;   
            Console.Write("Hello ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("World!");
            Console.ResetColor();
            PlayGame();


        }

        static void PlayGame()
        {
            
            Map map = new Map();
            Entity player = map.GetEntityById(1);

            while (player.State == LifeState.Alive)
            {
                map.ViewWorldMap();
                Direction direction = player.GetEntityInput();
                (int dx, int dy) offset = player.OffsetDirectionMapper(direction);
                map.TryPositionUpdate(offset, player);
               
            }
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player Health: {player.Health}");
        }
    }
}