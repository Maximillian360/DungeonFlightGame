namespace DungeonFlightGame
{
    internal class Program
    {
        public enum Direction { 
            Down, 
            Up, 
            Left, 
            Right,
            None
        }

        static void Main(string[] args)
        {
            //TODO: Refactor this abomination, especially the random values
            Direction playerInput;
            Random random = new Random();
            int mapLength = random.Next(10, 15);
            int mapWidth = random.Next(10, 15);
            int[,] worldMap = new int[mapLength, mapWidth];
            int worldMapRows = worldMap.GetLength(0);
            int worldMapCols = worldMap.GetLength(1);
            int playerX = 0;
            int playerY = 0;
            int playerHealth = 90;
            int enemyBaseHealth = 2;
            int difficultyMultiplier = 1;
            (int, int) playerOffset = (0, 0);
            MapGenerator(enemyBaseHealth, playerHealth, worldMap, playerX, playerY, difficultyMultiplier);
            bool gameRunning = true;
            while (gameRunning)
            {
                Console.Clear();
                Console.WriteLine("Map:");
                ViewMap(worldMap);
                Console.WriteLine("");
                Console.WriteLine($"Map Length: {mapLength}, Map Width: {mapWidth}");
                //TODO: Make this more ~elegant
                if ((playerY == worldMapRows - 1 && playerX == worldMapCols - 1) && playerHealth > 0)
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
                    Console.WriteLine($"Something went wrong!: inputValid");
                    continue;
                }

            }
        }

        static Direction GetPlayerDirection()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("W: for Up, S: for Down, D: for Right, A: for Left");
                string? temporaryPlayerInput = Console.ReadLine()?.ToLower();
                Direction direction = Direction.None;
                if (string.IsNullOrEmpty(temporaryPlayerInput))
                {
                    continue;
                }

                if (!(temporaryPlayerInput == "w" || temporaryPlayerInput == "s" || temporaryPlayerInput == "a" || temporaryPlayerInput == "d"))
                {
                    continue;
                }
                else
                {
                    switch (temporaryPlayerInput)
                    {
                        case "w":
                            direction = Direction.Up; break;
                        case "s":
                            direction = Direction.Down; break;
                        case "a":
                            direction = Direction.Left; break;
                        case "d":
                            direction = Direction.Right; break;
                    }
                    return direction;
                }
            }
        }

        static (int dy, int dx) DirectionOffsetMapper (Direction direction)
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

        static void PositionUpdate(int baseCell, ref int userHealth, int[,] worldMap,
                                   ref int userYPosition, ref int userXPosition, 
                                   int worldMapRows, int worldMapCols, 
                                   (int, int) playerOffset)
        {
            //Console.Clear();
            int checkNewYPos = userYPosition + playerOffset.Item1;
            int checkNewXPos = userXPosition + playerOffset.Item2;
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

        static bool ValidateNewPosition(int[,] worldMap, int newCell, int checkNewHealth, int checkNewX, int checkNewY, int worldMapRows, int worldMapCols)
        {
            return checkNewX >= 0 && checkNewY >= 0 && checkNewY < worldMapRows  && checkNewX < worldMapCols;
        }

        static void MapGenerator(int baseCell, int userHealth, int[,] worldMap, int userX, int userY, int healthDifficultyMultiplier)
        {
            //TODO: Make the RNG implementation "better"
            Random random = new Random();
            int enemyHealthModifier = 0;
            for (int i = 0; i < worldMap.GetLength(0); i++)
            {
                for (int j = 0; j < worldMap.GetLength(1); j++)
                {
                    if (i == userX && j == userY)
                    {
                        worldMap[i, j] = userHealth;
                    }
                    else
                    {
                        enemyHealthModifier = random.Next(1, 5);
                        worldMap[i, j] = (baseCell + enemyHealthModifier) * healthDifficultyMultiplier;
                    }
                }
            }
        }

        static void ViewMap(int[,] worldMapDimensions)
        {
            char padding = ' ';
            for (int i = 0; i < worldMapDimensions.GetLength(0); i++)
            {
                for (int j = 0; j < worldMapDimensions.GetLength(1); j++)
                {
                    Console.Write($"|{worldMapDimensions[i, j]}|".PadRight(4, padding));
                }
                Console.WriteLine("");
            }
        }
    }
}