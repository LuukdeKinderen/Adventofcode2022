namespace d06
{
    public class D06
    {
        public static void Start()
        {
            string line = File.ReadAllText("D06.txt");

            Console.WriteLine($"Part 1: {IndexWhereThereAreNumberOfUniqueCharacters(line, 4)}");
            Console.WriteLine($"Part 2: {IndexWhereThereAreNumberOfUniqueCharacters(line, 14)}");

        }

        private static int IndexWhereThereAreNumberOfUniqueCharacters(string input, int amount)
        {
            int index = 0;
            foreach (char streamChar in input)
            {
                var streamPiece = input.Skip(index).Take(amount).ToArray();

                if (streamPiece.Distinct().Count() == amount)
                {
                    break;
                }

                index++;
            }
            return index + amount;

        }

    }
}