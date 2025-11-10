namespace DungeonFlightGame;

internal class Program
{
    public enum Direction
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    private static void Main(string[] args)
    {
        //TODO: Refactor this abomination, especially the random values
        Direction playerInput;
        var random = new Random();
        var mapLength = random.Next(10, 15);
        var mapWidth = random.Next(10, 15);
        var worldMap = new int[mapLength, mapWidth];
        var worldMapRows = worldMap.GetLength(0);
        var worldMapCols = worldMap.GetLength(1);
        var playerX = 0;
        var playerY = 0;
        var playerHealth = 120;
        var enemyBaseHealth = 2;
        var difficultyMultiplier = 1;
        var playerOffset = (0, 0);
        MapGenerator(enemyBaseHealth, playerHealth, worldMap, playerX, playerY, difficultyMultiplier);
        var gameRunning = true;
        while (gameRunning)
        {
            Console.Clear();
            Console.WriteLine("Map:");
            ViewMap(worldMap);
            Console.WriteLine("");
            Console.WriteLine($"Map Length: {mapLength}, Map Width: {mapWidth}");
            //TODO: Make this more ~elegant
            if (playerY == worldMapRows - 1 && playerX == worldMapCols - 1 && playerHealth > 0)
            {
                gameRunning = false;
                Console.WriteLine("You Win!");
                break;
            }

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                gameRunning = false;
                Console.WriteLine("You Died!");
            }

            else if (playerHealth > 0)
            {
                playerInput = GetPlayerDirection();
                playerOffset = DirectionOffsetMapper(playerInput);
                PositionUpdate(enemyBaseHealth, ref playerHealth, worldMap,
                    ref playerY, ref playerX, worldMapRows, worldMapCols, playerOffset);
            }

            else
            {
                Console.WriteLine("Something went wrong!: inputValid");
            }
        }
    }

    private static Direction GetPlayerDirection()
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("W: for Up, S: for Down, D: for Right, A: for Left");
            var temporaryPlayerInput = Console.ReadLine()?.ToLower();
            var direction = Direction.None;
            if (string.IsNullOrEmpty(temporaryPlayerInput)) continue;
            if (temporaryPlayerInput is not ("w" or "s" or "a" or "d"))
            {
                continue;
            }
            else
            {
                direction = temporaryPlayerInput switch
                {
                    "w" => Direction.Up,
                    "s" => Direction.Down,
                    "a" => Direction.Left,
                    "d" => Direction.Right,
                    _ => Direction.None
                };
                return direction;
            }
        }
    }

    private static (int dy, int dx) DirectionOffsetMapper(Direction direction)
    {
        var playerOffset = direction switch
        {
            Direction.Up => (-1, 0),
            Direction.Down => (1, 0),
            Direction.Left => (0, -1),
            Direction.Right => (0, 1),
            _ => (0, 0)
        };
        return playerOffset;
    }

    private static void PositionUpdate(int baseCell, ref int userHealth, int[,] worldMap,
        ref int userYPosition, ref int userXPosition,
        int worldMapRows, int worldMapCols,
        (int, int) playerOffset)
    {
        Console.Clear();
        var checkNewYPos = userYPosition + playerOffset.Item1;
        var checkNewXPos = userXPosition + playerOffset.Item2;
        if (ValidateNewPosition(worldMap, baseCell, userHealth, checkNewXPos, checkNewYPos, worldMapRows, worldMapCols))
        {
            userHealth -= worldMap[checkNewYPos, checkNewXPos];
            userYPosition = checkNewYPos;
            userXPosition = checkNewXPos;
            worldMap[userYPosition, userXPosition] = userHealth;
            worldMap[userYPosition - playerOffset.Item1, userXPosition - playerOffset.Item2] = 0;
        }
        else
        {
            Console.WriteLine("Try Again!");
        }

        Console.WriteLine("");
    }

    private static bool ValidateNewPosition(int[,] worldMap, int newCell, int checkNewHealth, int checkNewX,
        int checkNewY, int worldMapRows, int worldMapCols)
    {
        return checkNewX >= 0 && checkNewY >= 0 && checkNewY < worldMapRows && checkNewX < worldMapCols;
    }

    private static void MapGenerator(int baseCell, int userHealth, int[,] worldMap, int userX, int userY,
        int healthDifficultyMultiplier)
    {
        //TODO: Make the RNG implementation "better"

        for (var i = 0; i < worldMap.GetLength(0); i++)
        for (var j = 0; j < worldMap.GetLength(1); j++)
            if (i == userX && j == userY)
            {
                worldMap[i, j] = userHealth;
            }
            else
            {
                var random = new Random();
                var enemyHealthModifier = 0;
                enemyHealthModifier = random.Next(1, 5);
                worldMap[i, j] = (baseCell + enemyHealthModifier) * healthDifficultyMultiplier;
            }
    }

    private static void ViewMap(int[,] worldMapDimensions)
    {
        var padding = ' ';
        for (var i = 0; i < worldMapDimensions.GetLength(0); i++)
        {
            for (var j = 0; j < worldMapDimensions.GetLength(1); j++)
                Console.Write($"|{worldMapDimensions[i, j]}|".PadRight(4, padding));
            Console.WriteLine("");
        }
    }
}