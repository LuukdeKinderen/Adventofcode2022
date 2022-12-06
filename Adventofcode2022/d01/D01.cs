namespace d01
{
    public class D01
    {
        private static readonly List<Elf> elves = new();
        public static void Start()
        {
            string[] lines = File.ReadAllLines("D01.txt");


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