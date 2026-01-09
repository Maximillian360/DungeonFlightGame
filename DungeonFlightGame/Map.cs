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
        Tile[,] WorldMap = new Tile?[WorldMapCols, WorldMapRows];
        

    }

    public Entity? GetEntity(int x, int y) => ValidatePosition(x, y) ? WorldMap[x, y].Entity : null;
    
    public bool ValidatePosition(int newX, int newY)
    {
        return newX >= 0 && newY >= 0 && newX < WorldMapCols && newY < WorldMapRows;
    }

    public void PositionUpdate((int, int) playerOffset)
    {
        if (!ValidatePosition(playerOffset.Item1, playerOffset.Item2))
        {
            Console.WriteLine("Error, Invalid Position: {playerOffset.Item1}, {playerOffset.Item2}!");
            return;
        }
        
        
    }
    
    public void ViewWorldMap()
    {
        
    }

}