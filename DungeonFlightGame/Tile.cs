namespace DungeonFlightGame;

public class Tile
{
    public Entity Entity { get; private set; }
    public Tile(Entity entity)
    {
        Entity = entity;
    }
}