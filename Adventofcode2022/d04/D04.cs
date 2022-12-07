namespace d04
{
    public class D04
    {
        public static void Start()
        {
            int contianingRanges = 0;
            int overlappingRanges = 0;
            string[] lines = File.ReadAllLines("D04.txt");
            foreach (var line in lines)
            {
                var pairs = line.Split(',');
                Range range1 = new Range(pairs[0].Split('-'));
                Range range2 = new Range(pairs[1].Split('-'));

                if (Contains(range1, range2))
                {
                    contianingRanges++;
                }
                if (Overlap(range1, range2))
                {
                    overlappingRanges++;
                }
            }
            Console.WriteLine("Containing:  " + contianingRanges);
            Console.WriteLine("Overlapping: " + overlappingRanges);
        }
        private static bool Contains(Range a, Range b)
        {
            return a.Start <= b.Start && a.End >= b.End || b.Start <= a.Start && b.End >= a.End;
        }
        private static bool Overlap(Range a, Range b)
        {
            return a.Start <= b.End && a.Start >= b.Start || b.Start <= a.End && b.Start >= a.Start;
        }

        class Range
        {
            public int Start;
            public int End;

            public Range(string[] pair)
            {
                Start = int.Parse(pair[0]);
                End = int.Parse(pair[1]);
            }
        }
    }
}