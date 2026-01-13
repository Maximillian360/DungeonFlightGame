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
        GenerateMap();
        
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

    public void GenerateMap()
    {
        var player = Entity.PlayerFactory(); 
        WorldMap[0, 0] = new Tile();
        WorldMap[0, 0].Entity = player;
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                if (i == 0 && j == 0) continue;
                
                WorldMap[i, j] = new Tile();
                
                if (!IsPositionValid(i, j))
                {
                    Console.WriteLine("Something went wrong!");
                    continue;
                }
                var enemy = Entity.EnemyFactory(i, j);
                WorldMap[i, j].Entity = enemy;
            }
        }
    }

    public void ViewWorldMap()
    {
        char padding = ' ';
        Console.WriteLine($"Map  size: Rows: {WorldMapRows} * Cols: {WorldMapCols} = {WorldMapRows * WorldMapCols}");
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                Entity? e = GetEntity(i, j);
                Console.Write(e == null ? "| |".PadRight(4, padding) : $"|{e.Glyph}|".PadRight(4, padding));
            }
            Console.WriteLine();
        }
    }
}