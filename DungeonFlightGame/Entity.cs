namespace DungeonFlightGame;

public class Entity
{
    public string Name { get; }
    public Type Type { get; }
    public int Id { get; private set; }
    public char Glyph { get; }
    public int Health { get; private set; }
    public LifeState State { get; set; }
    public int Damage { get; private set; }
    public Direction Direction { get; private set; }
    
    public Point Position { get; private set; }
    public Point Offset { get; private set; }
    
    private static int _counter = 1;

    public Entity(string name, Type type, char glyph, int health, Point position, Point offset, Direction direction, LifeState state = LifeState.Alive)
    {
        Name = name;
        Type = type;
        Id = _counter;
        Glyph = glyph;
        Health = health;
        State = state;
        Damage = health;
        Position = position;
        Offset = offset;
        Direction = direction;
        _counter++;
    }

    public void TakeDamage(Entity entity)
    {
        Health -= entity.Damage;
        if (Health <= 0)
        {
            State = LifeState.Dead;
        }
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

    public Point OffsetDirectionMapper(Direction direction)
    {
        Point entityOffset = direction switch
        {
            Direction.Up => new Point(-1, 0),
            Direction.Down => new Point (1, 0),
            Direction.Left => new Point (0, -1),
            Direction.Right => new Point (0, 1),
            _ => new Point (0, 0)
        };
        return entityOffset;
    }

    public void PositionUpdate(Point newPosition) => Position = newPosition;
    
    public static Entity EnemyFactory(int x, int y)
    {
        Random random = new Random();
        return new Entity("Enemy", Type.Enemy, '*', random.Next(5, 10), new Point(x, y), new Point(0,0), Direction.None, LifeState.Alive);
    }
    
    public static Entity PlayerFactory()
    {
        Random random = new Random();
        return new Entity("Player", Type.Player, '@', random.Next(60, 65), new Point(0,0), new Point(0,0), Direction.None, LifeState.Alive);
    }

    public (int, int) GetEntityPosition(Entity entity)
    {
        return (entity.Position.X, entity.Position.Y);
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

public enum LifeState
{
    Alive,
    Dead
}

