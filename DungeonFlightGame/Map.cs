using System.Reflection.Metadata.Ecma335;

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

    public Entity? GetEntityById(int id)
    {
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                Tile? tile = GetTile(i, j);
                if (tile == null) continue;
                if (tile.Entity == null) continue;
                if (tile.Entity.Id != id) continue;
                return tile.Entity;
            }
        }
        return null;
    }

    public Tile? GetTile(int x, int y) => IsPositionInside(x, y) ? WorldMap[x, y] : null;
    
    public bool IsPositionInside(int newX, int newY) => newX >= 0 && newY >= 0 && newX < WorldMapRows && newY < WorldMapCols;
    
    
    public bool IsPositionValid(int x, int y)
    {
        if (!IsPositionInside(x, y))
        {
            Console.WriteLine($"Error, Position: {x}, {y} is out of bounds!");
            return false;
        }

        if (GetTile(x, y) == null)
        {
            Console.WriteLine($"Error, Position: {x}, {y} has no Tile!");
            return false;
        }

        if (GetEntity(x, y) != null)
        {
            Console.WriteLine($"Position: {x}, {y} is occupied!");
            return false;
        }
        return true;
    }


    public void TryPositionUpdate(Point newPoint, Entity entity)
    {
        int currentX = entity.Position.X;
        int currentY = entity.Position.Y;
        int newPositionX = newPoint.X;
        int newPositionY = newPoint.Y;
        newPositionX += currentX;
        newPositionY += currentY;

        if (!IsPositionInside(newPositionX, newPositionY))
        {
            Console.WriteLine("Position is out of bounds!");
            return;
        }

        if (GetTile(newPositionX, newPositionY) == null)
        {
            Console.WriteLine("No tile found!");
            return;
        }
        
        if (GetEntity(newPositionX, newPositionY) != null)
        {
            Entity idleEntity = GetEntity(newPositionX, newPositionY);
            idleEntity.TakeDamage(entity);
            entity.TakeDamage(idleEntity);
            
        }
        
        WorldMap[newPositionX, newPositionY].Entity = WorldMap[currentX, currentY].Entity;
        Point newPositionPoint = new Point(newPositionX, newPositionY);
        WorldMap[newPositionX, newPositionY].Entity.PositionUpdate(newPositionPoint);
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
        Console.Clear();
        char padding = ' ';
        Console.WriteLine($"Map  size: Rows: {WorldMapRows} * Cols: {WorldMapCols} = {WorldMapRows * WorldMapCols}");
        Entity? player = GetEntityById(1);
        if (player == null)
        {
            Console.WriteLine("Player not found!");
            return;
        }
        Console.WriteLine($"Entity: {player.Name}, Health: {player.Health}");
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