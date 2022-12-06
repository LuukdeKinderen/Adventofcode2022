namespace d03
{
    public class D03
    {
        public static void Start()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            int totalValue = 0;

            string[] lines = File.ReadAllLines("D03.txt");
            foreach (string line in lines)
            {
                string top = line.Substring(0, line.Length / 2);
                string bottom = line.Substring(line.Length / 2);

                Console.Write(top + " " + bottom + " ");

                var character = bottom.First(c => top.Contains(c));

                int value = Value(character);
                Console.WriteLine(character + " " + value);
                totalValue += value;
            }
            Console.WriteLine(totalValue);
        }
        private static void PartTwo()
        {
            string[] lines = File.ReadAllLines("D03.txt");

            int totalValue = 0;

            int lineCount = 0;
            while (lineCount < lines.Length)
            {
                string[] group = new string[]
                {
                    lines[lineCount++],
                    lines[lineCount++],
                    lines[lineCount++],
                };

                foreach (string line in group)
                {
                    Console.WriteLine(line);
                }
                int groupValue = 0;
                foreach(char c in group[0])
                {
                    if (group[1].Contains(c) && group[2].Contains(c))
                    {
                        groupValue = Value(c);
                        break;
                    }
                }
                totalValue += groupValue;
                Console.WriteLine();

            }
            Console.WriteLine(totalValue);
        }

        private static int Value(char character)
        {
            if (char.IsLower(character))
            {
                return (int)character % 32;
            }
            else
            {
                return (int)character % 32 + 26;
            }
        }
    }
}