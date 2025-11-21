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
        this.Name = name;
        this.Type = type;
        this.Glyph = glyph;
        this.PositionX = positionX;
        this.PositionY = positionY;
        this.Health = health;
        this.Offset = offset;
        this.Direction = direction;
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

struct Position 
{
    public int x;
    public int y;
}