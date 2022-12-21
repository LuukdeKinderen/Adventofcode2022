using System.Text.RegularExpressions;

namespace d05
{
    public class D05
    {
        public static void Start()
        {
            string[] lines = File.ReadAllLines("D05.txt");
            string[] shipLines = ShipLines(lines);
            string[] instructionLines = InstructionLines(lines);

            Stack<char>[] ship = Ship(shipLines);
            IEnumerable<Instruction> instructions = Instructions(instructionLines);

            printShip(ship);
            //DoWork(ship, instructions);
            DoWorkV2(ship, instructions);

            Console.WriteLine("Total:");
            foreach (var stack in ship)
            {
                Console.Write(stack.Pop());
            }
        }

        private static void DoWork(Stack<char>[] ship, IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.amount; i++)
                {
                    char temp = ship[instruction.from - 1].Pop();
                    ship[instruction.to - 1].Push(temp);
                }
                printShip(ship);
            }
        }

        private static void DoWorkV2(Stack<char>[] ship, IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                List<char> temp = new List<char>();
                for (int i = 0; i < instruction.amount; i++)
                {
                    temp.Add(ship[instruction.from - 1].Pop());
                }

                for (int i = temp.Count() - 1; i > -1; i--)
                {
                    ship[instruction.to - 1].Push(temp[i]);
                }

                printShip(ship);
            }
        }

        private static void printShip(Stack<char>[] ship)
        {
            foreach (var stack in ship)
            {
                foreach (var crate in stack)
                {
                    Console.Write(crate.ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static IEnumerable<Instruction> Instructions(string[] instructionLines)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (var line in instructionLines)
            {
                instructions.Add(new Instruction(line));
            }
            return instructions;
        }

        private static Stack<char>[] Ship(string[] shipLines)
        {
            string lastline = shipLines[shipLines.Length - 1];
            int deckWidth = int.Parse(lastline[lastline.Length - 2].ToString());
            Stack<char>[] ship = new Stack<char>[deckWidth];
            for (int y = 0; y < ship.Length; y++)
            {
                List<char> stack = new List<char>();
                for (int x = 0; x < shipLines.Length; x++)
                {
                    Regex regex = new Regex("[A-Z]");
                    char c = shipLines[x][y * 4 + 1];
                    if (regex.IsMatch(c.ToString()))
                    {
                        stack = stack.Prepend(c).ToList();
                        //ship[y] = ship[y].Append(c);
                        //ship[y].Push(c);
                    }
                }
                ship[y] = new Stack<char>(stack);
            }
            return ship;
        }

        private static string[] ShipLines(string[] lines)
        {
            List<string> ship = new();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                ship.Add(line);
            }
            return ship.ToArray();
        }
        private static string[] InstructionLines(string[] lines)
        {
            List<string> instructions = new();
            bool isInstruction = false;
            foreach (var line in lines)
            {
                if (isInstruction)
                {
                    instructions.Add(line);
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInstruction = true;
                }
            }
            return instructions.ToArray();
        }

        class Instruction
        {
            public int amount { get; }
            public int from { get; }
            public int to { get; }

            public Instruction(string line)
            {
                Regex instructionRegex = new Regex(@"^move (?<amount>[\d]+) from (?<from>[\d]+) to (?<to>[\d]+)$");
                GroupCollection matches = instructionRegex.Match(line).Groups;

                amount = int.Parse(matches["amount"].Value);
                from = int.Parse(matches["from"].Value);
                to = int.Parse(matches["to"].Value);
            }
        }
    }
}