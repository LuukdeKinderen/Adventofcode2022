namespace _01
{
    internal class Program
    {
        public static List<Elf> elves = new List<Elf>();
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("Input.txt");


            Elf current = new Elf();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(current);
                    current = new Elf();
                }
                else
                {
                    current.AddSnack(line);
                }
            }

            foreach (var elf in elves)
            {
                PrintElf(elf);
            }

            var maxElf = elves.MaxBy(e => e.TotalCalories);
            Console.WriteLine("Elf with max calories:");
            PrintElf(maxElf);


            var topThreeElves = elves.OrderByDescending(e => e.TotalCalories).Take(3);

            Console.WriteLine("Top three elves: ");
            foreach (var elf in topThreeElves)
            {
                PrintElf(elf);
            }

            Console.WriteLine("Total of top three elves: ");
            Console.WriteLine(topThreeElves.Sum(e => e.TotalCalories));
        }


        private static void PrintElf(Elf elf)
        {
            Console.WriteLine($"elf {elves.IndexOf(elf)}");
            Console.Write("\t");
            foreach (var snack in elf.snacks)
            {
                Console.Write(snack + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"\tTotal: {elf.TotalCalories}");
            Console.WriteLine();
        }

    }
}