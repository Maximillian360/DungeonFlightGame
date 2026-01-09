namespace DungeonFlightGame;

public class Tile
{
    public Entity? Entity { get; private set; }
    public Tile(Entity? entity = null)
    {
        Entity = entity;
    }
}