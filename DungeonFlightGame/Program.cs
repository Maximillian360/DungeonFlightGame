namespace DungeonFlightGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string playerInput = "";
            int[,] worldMap = new int[7, 4];
            int worldMapRows = worldMap.GetLength(1);
            int worldMapCols = worldMap.GetLength(0);
            int userXPosition = 0;
            int userYPosition = 0;
            int userHealth = 90;
            int baseCell = 2;
            bool gameRunning = true;
            MapGenerator(baseCell, userHealth, worldMap, userXPosition, userYPosition);
            while (gameRunning)
            {
                Console.WriteLine("Old Map:");
                ViewMap(worldMap);
                Console.WriteLine("");
                playerInput = takePlayerInput();

                if (userHealth <= 0)
                {
                    userHealth = 0;
                    gameRunning = false;
                    Console.WriteLine("You Died!");
                }

                else if (userHealth > 0)
                {
                    updateMap(baseCell, ref userHealth, ref worldMap, 
                              ref userYPosition, ref userXPosition, ref worldMapRows, ref worldMapCols, playerInput);
                }

                else
                {
                    Console.WriteLine($"Something went wrong!: inputValid");
                    continue;
                }

            }
        }

        //TODO: Refactor the player input choices to enums
        static string takePlayerInput()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("W: for Up, S: for Down, D: for Right, A: for Left");
                string? temporaryPlayerInput = Console.ReadLine()?.ToLower();
                if (string.IsNullOrEmpty(temporaryPlayerInput)) {
                    continue;
                }

                if (!(temporaryPlayerInput == "w" || temporaryPlayerInput == "s" || temporaryPlayerInput == "a" || temporaryPlayerInput == "d"))
                {
                    
                    continue;
                }
                return temporaryPlayerInput;
            }
            
        }

        //TODO: Remove redundant code in the nested conditionals
        static void updateMap(int baseCell, ref int userHealth, ref int[,] worldMap,
                              ref int userYPosition, ref int userXPosition, 
                              ref int worldMapRows, ref int worldMapCols, 
                              string playerInput)
        {
            Console.Clear();
            int checkNewXPos = userXPosition;
            int checkNewYPos = userYPosition;

            Console.WriteLine("");
            if (playerInput == "s")
            {
                checkNewYPos += 1;
                if (validateNewPosition(worldMap, baseCell, userHealth, checkNewYPos, checkNewXPos, worldMapRows, worldMapCols))
                {
                    userYPosition = checkNewYPos;
                    userHealth -= worldMap[checkNewYPos, checkNewXPos];
                    worldMap[userYPosition, userXPosition] = userHealth;
                    worldMap[userYPosition - 1, userXPosition] = 0;
                }
                else
                {
                    Console.WriteLine("Try Again!");
                }
            }

            else if (playerInput == "w")
            {
                checkNewYPos -= 1;
                if (validateNewPosition(worldMap, baseCell, userHealth, checkNewYPos, checkNewXPos, worldMapRows, worldMapCols))
                {

                    userYPosition = checkNewYPos;
                    userHealth -= worldMap[checkNewYPos, checkNewXPos];
                    worldMap[userYPosition, userXPosition] = userHealth;
                    worldMap[userYPosition + 1, userXPosition] = 0;
                }
                else
                {
                    Console.WriteLine("Try Again!");
                }
            }

            else if (playerInput == "d")
            {
                checkNewXPos += 1;
                if (validateNewPosition(worldMap, baseCell, userHealth, checkNewYPos, checkNewXPos, worldMapRows, worldMapCols))
                {

                    userXPosition = checkNewXPos;
                    userHealth -= worldMap[checkNewYPos, checkNewXPos];
                    worldMap[userYPosition, userXPosition] = userHealth;
                    worldMap[userYPosition, userXPosition - 1] = 0;
                }
                else
                {
                    Console.WriteLine("Try Again!");
                }
            }

            else if (playerInput == "a")
            {
                checkNewXPos -= 1;
                if (validateNewPosition(worldMap, baseCell, userHealth, checkNewYPos, checkNewXPos, worldMapRows, worldMapCols))
                {

                    userXPosition = checkNewXPos;
                    userHealth -= worldMap[checkNewYPos, checkNewXPos];
                    worldMap[userYPosition, userXPosition] = userHealth;
                    worldMap[userYPosition, userXPosition + 1] = 0;
                }
                else
                {
                    Console.WriteLine("Try Again!");
                }
            }
            Console.WriteLine("");
           

        }

        static bool validateNewPosition(int[,] worldMap, int newCell, int checkNewHealth, int checkNewYPos, int checkNewXPos, int worldMapRows, int worldMapCols)
        {
            return checkNewXPos >= 0 && checkNewYPos >= 0 && checkNewYPos < worldMapCols && checkNewXPos < worldMapRows;
        }

        static void MapGenerator(int baseCell, int userHealth, int[,] worldMap, int userX, int userY)
        {
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
                        worldMap[i, j] = baseCell;
                    }

                }
            }
        }

        static void ViewMap(int[,] worldMapDimensions)
        {
            for (int i = 0; i < worldMapDimensions.GetLength(0); i++)
            {
                for (int j = 0; j < worldMapDimensions.GetLength(1); j++)
                {
                    Console.Write($"{worldMapDimensions[i, j]} ");
                }
                Console.WriteLine("");
            }
        }
    }
}