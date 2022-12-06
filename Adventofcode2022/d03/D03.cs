namespace d03
{
    public class D03
    {
        static int totalValue = 0;
        public static void Start()
        {
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