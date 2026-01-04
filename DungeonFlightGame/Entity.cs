namespace DungeonFlightGame;

public class Entity
{
    public string Name { get; }
    public string Type { get; }
    public char Glyph { get; }
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
    public int Health { get; private set; }
    public (int, int) Offset { get; private set; }
    public Direction Direction { get; private set; }

    public Entity(string name, string type, char glyph, int positionX, int positionY, int health, (int, int) offset, Direction direction)
    {
        Name = name;
        Type = type;
        Glyph = glyph;
        PositionX = positionX;
        PositionY = positionY;
        Health = health;
        Offset = offset;
        Direction = direction;
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
