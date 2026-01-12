namespace DungeonFlightGame;

public class Entity
{
    public string Name { get; }
    public Type Type { get; }
    public char Glyph { get; }
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
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
        Damage = health;
        Offset = offset;
        Direction = direction;
    }

    public void EntityTakeDamage(Entity entity)
    {
        Health -= entity.Damage;
    }

    public Direction GetEntityInput()
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("W: for Up, S: for Down, D: for Right, A: for Left. ");
            string? temporaryInput = Console.ReadLine()?.ToUpper();
            Direction direction = Direction.None;
            if (string.IsNullOrEmpty(temporaryInput))
            {
                Console.WriteLine("Input cannot be empty or null!");
                continue;
            }

            if (!(temporaryInput == "W" || temporaryInput == "S" || temporaryInput == "A" || temporaryInput == "D"))
            {
                Console.WriteLine($"Input not recognized: Player Input: {temporaryInput}");
                continue;
            }

            direction = temporaryInput switch
            {
                "W" => Direction.Up,
                "S" => Direction.Down,
                "A" => Direction.Left,
                "D" => Direction.Right,
                _ => direction
            };
            
            return direction;
        }
    }

    public (int dx, int dy) OffsetDirectionMapper(Direction direction)
    {
        var entityOffset = direction switch
        {
            Direction.Up => (-1, 0),
            Direction.Down => (1, 0),
            Direction.Left => (0, -1),
            Direction.Right => (0, 1),
            _ => (0, 0)
        };
        return entityOffset;
    }

    public void PositionUpdate(int newX, int newY)
    {
        PositionX = newX;
        PositionY = newY;
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


