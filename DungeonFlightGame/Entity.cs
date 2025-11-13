namespace DungeonFlightGame;

public class Entity
{
    private string entityName;
    private int entityXPosition;
    private int entityYPosition;
    private int entityHealth;
    private (int, int) entityOffset;
    private Direction entityDirection;

    public Entity(string entityName, int entityXPosition, int entityYPosition, int entityHealth, (int, int) entityOffset, Direction entityDirection)
    {
        this.entityName = entityName;
        this.entityXPosition = entityXPosition;
        this.entityYPosition = entityYPosition;
        this.entityHealth = entityHealth;
        this.entityOffset = entityOffset;
        this.entityDirection = entityDirection;
    }

    public void GetEntityDirection()
    {
        
    }

    public void EntityMove()
    {
        
        
    }

    public void EntityTakeDamage()
    {
        
    }
    
    
}

public enum Direction { 
    Down, 
    Up, 
    Left, 
    Right,
    None
}