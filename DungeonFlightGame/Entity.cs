namespace DungeonFlightGame;

public class Entity
{
    public string Name { get; }
    public Type Type { get; }
    public char Glyph { get; }
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
    public int Health { get; private set; }
    public (int, int) Offset { get; private set; }
    public Direction Direction { get; private set; }

    public Entity(string name, Type type, char glyph, int positionX, int positionY, int health, (int, int) offset, Direction direction)
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

    public static Entity EnemyFactory(int x, int y)
    {
        Random random = new Random();
        return new Entity("Enemy", Type.Enemy, '*', x, y, random.Next(5, 10), (0,0), Direction.None);
    }
    
    public static Entity PlayerFactory()
    {
        Random random = new Random();
        return new Entity("Player", Type.Player, '@', 0, 0, random.Next(60, 65), (0,0), Direction.None);
    }
    
}

public enum Direction { 
    Down, 
    Up, 
    Left, 
    Right,
    None
}

public enum Type
{
    Enemy,
    Player
}


