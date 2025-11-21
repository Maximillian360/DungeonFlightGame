namespace DungeonFlightGame;

public class Map
{
    public int WorldMapCols { get; }
    public int WorldMapRows { get; }
    public int[,] WorldMap { get; }
    
    public  Map(int worldMapCols, int worldMapRows, int[,] worldMap)
    {
        this.WorldMapCols = worldMapCols;
        this.WorldMapRows = worldMapRows;
        this.WorldMap = worldMap;
    }
    
    public void ViewWorldMap()
    {
        
    }

}