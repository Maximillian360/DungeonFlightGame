namespace DungeonFlightGame;

public class Map
{
    public int WorldMapCols { get; }
    public int WorldMapRows { get; }
    public Tile?[,] WorldMap { get; }
    
    public Map()
    {
        Random random = new Random();
        WorldMapCols = random.Next(10, 15);
        WorldMapRows = random.Next(10, 15);
        Tile?[,] worldMap = new Tile?[WorldMapCols, WorldMapRows];

    }
    
    public void ValidateNewPosition()
    {
        
    }

    public void PositionUpdate()
    {
        
    }
    
    public void ViewWorldMap()
    {
        
    }

}