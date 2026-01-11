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

    public bool IsTileOccupied(int x, int y)
    {
        return GetEntity(x, y) != null;
    }
    
    public bool IsPositionInside(int newX, int newY)
    {
        return newX >= 0 && newY >= 0 && newX < WorldMapCols && newY < WorldMapRows;
    }

    public bool TryPlaceEntity(Entity entity, (int x, int y) entityPosition)
    {
        return true;
    }

    public void TryPositionUpdate((int x, int y) newPosition, Entity entity)
    {
        int currentX = entity.PositionX;
        int currentY = entity.PositionY;
        if (!IsPositionInside(newPosition.x, newPosition.y))
        {
            Console.WriteLine($"Error, Invalid Position: {newPosition.x}, {newPosition.y}!");
            return;
        }
        
        if (IsTileOccupied(newPosition.x, newPosition.y))
        {
            Console.WriteLine($"Position: {newPosition.x}, {newPosition.y} is occupied!");
            return;
        }
        WorldMap[newPosition.x, newPosition.y].Entity = WorldMap[currentX, currentY].Entity;
        WorldMap[currentX, currentY].Entity = null;
    }

    public void MapMaker()
    {
        for (int i = 0; i < WorldMapCols; i++)
        {
            for (int j = 0; j < WorldMapRows; j++)
            {
                WorldMap[i, j] = new Tile();
                if (!IsPositionInside(i, j))
                {
                    Console.WriteLine($"Error, Invalid Position: {i}, {j}!");
                    continue;
                }

                if (IsTileOccupied(i, j))
                {
                    Console.WriteLine($"Position: {i}, {j} is occupied!");
                    continue;
                }

                if (i == 0 && j == 0)
                {
                    Entity player = Entity.PlayerFactory(); 
                }

                else
                {
                    Entity enemy = Entity.EnemyFactory(i, j);
                }

            }
        }
    }



    public void ViewWorldMap()
    {
        
    }

}