namespace DungeonFlightGame;

public class Map
{
    public int WorldMapCols { get; }
    public int WorldMapRows { get; }
    public Tile[,] WorldMap { get; }
    
    public Map()
    {
        Random random = new Random();
        WorldMapCols = random.Next(15, 20);
        WorldMapRows = random.Next(15, 20);
        WorldMap = new Tile[WorldMapRows, WorldMapCols];
        MapMaker();
        
    }

    public Entity? GetEntity(int x, int y) => IsPositionInside(x, y) ? WorldMap[x, y].Entity : null;

    public bool IsTileOccupied(int x, int y) => GetEntity(x, y) != null;
    
    public bool IsPositionInside(int newX, int newY) => newX >= 0 && newY >= 0 && newX < WorldMapRows && newY < WorldMapCols;
    
    
    public bool IsPositionValid(int x, int y)
    {
        if (!IsPositionInside(x, y))
        {
            Console.WriteLine($"Error, Position: {x}, {y} is out of bounds!");
            return false;
        }

        if (IsTileOccupied(x, y))
        {
            Console.WriteLine($"Position: {x}, {y} is occupied!");
            return false;
        }
        
        return true;
    }


    public void TryPositionUpdate((int x, int y) newPosition, Entity entity)
    {
        int currentX = entity.PositionX;
        int currentY = entity.PositionY;
        
        // if (!IsPositionInside(newPosition.x, newPosition.y))
        // {
        //     Console.WriteLine($"Error, Invalid Position: {newPosition.x}, {newPosition.y}!");
        //     return;
        // }
        //
        // if (IsTileOccupied(newPosition.x, newPosition.y))
        // {
        //     Console.WriteLine($"Position: {newPosition.x}, {newPosition.y} is occupied!");
        //     return;
        // }
        
        if (WorldMap[currentX, currentY].Entity == null)
        {
            Console.WriteLine("No entity found in source coordinates!");
            return;
        }
        
        if (!IsPositionValid(newPosition.x, newPosition.y))
        {
            Console.WriteLine("Something went wrong!");
            return;
        }
        
        if (WorldMap[currentX, currentY].Entity != entity)
        {
            Console.WriteLine($"Entity at position: {newPosition.x}, {newPosition.y} is not equal to {entity.PositionX}, {entity.PositionY}!");
            return;
        }
        
        WorldMap[newPosition.x, newPosition.y].Entity = WorldMap[currentX, currentY].Entity;
        WorldMap[newPosition.x, newPosition.y].Entity.PositionUpdate(newPosition.x, newPosition.y);
        WorldMap[currentX, currentY].Entity = null;
    }

    public void MapMaker()
    {
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                WorldMap[i, j] = new Tile();
                // if (!IsPositionInside(i, j))
                // {
                //     Console.WriteLine($"Error, Invalid Position: {i}, {j}!");
                //     continue;
                // }
                //
                // if (IsTileOccupied(i, j))
                // {
                //     Console.WriteLine($"Position: {i}, {j} is occupied!");
                //     continue;
                // }

                if (!IsPositionValid(i, j))
                {
                    Console.WriteLine("Something went wrong!");
                    continue;
                }
                
                if (i == 0 && j == 0)
                {
                    Entity player = Entity.PlayerFactory(); 
                    WorldMap[i, j].Entity = player;
                }

                else
                {
                    Entity enemy = Entity.EnemyFactory(i, j);
                    WorldMap[i, j].Entity = enemy;
                }
            }
        }
    }

    public void ViewWorldMap()
    {
        char padding = ' ';
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                Entity? e = GetEntity(i, j);
                Console.WriteLine(e == null ? "| |".PadRight(4, padding) : $"|{e.Glyph}|".PadRight(4, padding));
            }
            Console.WriteLine();
        }
    }

}