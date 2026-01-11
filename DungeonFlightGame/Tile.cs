namespace DungeonFlightGame;

public class Tile
{
    public Entity? Entity { get; set; }
    public Tile(Entity? entity = null)
    {
        Entity = entity;
    }

    public void SetEntityNull()
    {
        Entity = null;
    }
}