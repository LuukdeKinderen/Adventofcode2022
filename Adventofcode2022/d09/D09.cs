using System;
using System.Text;

namespace d09
{
    public class D09
    {
        private const int MillisecondsTimeout = 1000;

        public static void Start()
        {
            var lines = File.ReadAllLines("D09.txt");

            Rope shortRope = new Rope(2);
            Rope longRope = new Rope(10);

            foreach (var command in lines)
            {
                Direction direction = command[0] switch
                {
                    'U' => Direction.Up,
                    'D' => Direction.Down,
                    'R' => Direction.Right,
                    'L' => Direction.Left,
                    _ => throw new NotImplementedException()
                };
                int amount = int.Parse(command.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    longRope.Move(direction);
                    shortRope.Move(direction);
                }
            }
            VisiualizeRope(longRope);
            Console.WriteLine(((Tail)longRope.RopePieces.Last()).History.Distinct().Count());

            VisiualizeRope(shortRope);
            Console.WriteLine(((Tail)shortRope.RopePieces.Last()).History.Distinct().Count());
        }

        public static void VisiualizeRope(Rope rope)
        {
            var visualization = rope.Visualization();

            for (int y = visualization.GetLength(1) - 1; y > 0; y--)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < visualization.GetLength(0); x++)
                {
                    sb.Append(visualization[x, y]);
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }

    public class Rope
    {
        public RopePiece[] RopePieces { get; }
        public readonly uint Length;

        public Rope(uint length)
        {
            Length = length;
            RopePieces = new RopePiece[Length];
            for (int i = 0; i < Length; i++)
            {
                RopePieces[i] = new RopePiece();
            }
            RopePieces[RopePieces.Length - 1] = new Tail();
        }

        public void Move(Direction direction)
        {
            //move Head
            var Head = RopePieces[0];
            switch (direction)
            {
                case Direction.Left:
                    Head.Position = new(Head.Position.X - 1, Head.Position.Y);
                    break;
                case Direction.Right:
                    Head.Position = new(Head.Position.X + 1, Head.Position.Y);
                    break;
                case Direction.Up:
                    Head.Position = new(Head.Position.X, Head.Position.Y + 1);
                    break;
                case Direction.Down:
                    Head.Position = new(Head.Position.X, Head.Position.Y - 1);
                    break;
            }

            //Move rope
            for (int i = 1; i < Length; i++)
            {
                int newX = RopePieces[i].Position.X + (RopePieces[i].Position.X - RopePieces[i - 1].Position.X) switch
                {
                    < 0 => 1,
                    > 0 => -1,
                    _ => 0,
                };

                int newY = RopePieces[i].Position.Y + (RopePieces[i].Position.Y - RopePieces[i - 1].Position.Y) switch
                {
                    < 0 => 1,
                    > 0 => -1,
                    _ => 0,
                };

                RopePieces[i].Position = new Position(newX, newY);
            }


        }
        public char[,] Visualization()
        {
            Tail Tail = (Tail)RopePieces.Last();

            int Hight = Tail.History.SelectMany((a, i) => Tail.History.Skip(i + 1).Select((b) => Math.Abs(a.Y - b.Y))).Max() + (int)(Length * 2);
            int Width = Tail.History.SelectMany((a, i) => Tail.History.Skip(i + 1).Select((b) => Math.Abs(a.X - b.X))).Max() + (int)(Length * 2);

            char[,] chars = new char[Width, Hight];

            for (int y = 0; y < Hight; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var ActualX = x - Math.Abs(Tail.History.Min(h => h.X)) - (int)Length;
                    var ActualY = y - Math.Abs(Tail.History.Min(h => h.Y)) - (int)Length;

                    Position position = new(ActualX, ActualY);
                    RopePiece? ropePiece = RopePieces.FirstOrDefault(rp => rp.Position == position);


                    if (ropePiece != null)
                    {
                        int index = Array.IndexOf(RopePieces, ropePiece);
                        chars[x, y] = Convert.ToString(index)[0];
                    }

                    else if (position == new Position(0, 0))
                    {
                        chars[x, y] = 's';
                    }
                    else if (Tail.History.Any(h => h == position))
                    {
                        chars[x, y] = '#';
                    }
                    else
                    {
                        chars[x, y] = '.';
                    }
                }
            }
            return chars;

        }
    }



    public class RopePiece
    {
        public virtual Position Position { get; set; }

        public RopePiece()
        {
            Position = new();
        }
    }
    public class Tail : RopePiece
    {
        private Position _position;
        public override Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                _history.Add(value);
                _position = value;
            }
        }

        private readonly List<Position> _history = new();

        public IEnumerable<Position> History => _history;

        public Tail()
        {
            _position = new();
            Position = new();
        }
    }
    public record Position(int X = 0, int Y = 0);

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}