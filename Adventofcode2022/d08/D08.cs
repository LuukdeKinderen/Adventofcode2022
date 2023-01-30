using System.Runtime.CompilerServices;

namespace d08
{
    public class D08
    {
        public static void Start()
        {
            var lines = File.ReadAllLines("D08.txt");

            Forrest forrest = new Forrest(lines);


            int xlength = lines[0].Length;
            int ylength = lines.Length;

            int vissableTrees = 0;
            for (int x = 0; x < xlength; x++)
            {
                for (int y = 0; y < ylength; y++)
                {
                    if (forrest.IsTreeVisable(x, y))
                    {
                        vissableTrees++;
                    }
                }
            }
            Console.WriteLine($"Vissable trees: {vissableTrees}");

            var scenicScores = forrest.Trees.Select(t => forrest.ScenicScore(t.x, t.y));

            Console.WriteLine($"Max scenic score: {scenicScores.Max()}");
        }

        public class Forrest
        {


            public IEnumerable<Tree> Trees { get; }


            public Forrest(string[] forrest)
            {
                int xlength = forrest[0].Length;
                int ylength = forrest.Length;

                var trees = new List<Tree>();

                for (int y = 0; y < ylength; y++)
                {
                    for (int x = 0; x < xlength; x++)
                    {
                        int hight = int.Parse(forrest[y][x].ToString());
                        Tree tree = new Tree(x, y, hight);
                        trees.Add(tree);
                    }
                }
                Trees = trees;
            }

            public bool IsTreeVisable(int x, int y)
            {

                IEnumerable<Tree> TreesAboveTree = Trees.Where(t => t.x == x && t.y > y);
                IEnumerable<Tree> TreesBelowTree = Trees.Where(t => t.x == x && t.y < y);
                IEnumerable<Tree> TreesRightOfTree = Trees.Where(t => t.x > x && t.y == y);
                IEnumerable<Tree> TreesLeftOfTree = Trees.Where(t => t.x < x && t.y == y);

                Tree tree = Trees.Single(t => t.x == x && t.y == y);

                return !TreesAboveTree.Any(ta => ta.Hight >= tree.Hight) ||
                    !TreesBelowTree.Any(tb => tb.Hight >= tree.Hight) ||
                    !TreesRightOfTree.Any(tr => tr.Hight >= tree.Hight) ||
                    !TreesLeftOfTree.Any(tl => tl.Hight >= tree.Hight);
            }

            public int ScenicScore(int x, int y)
            {

                Tree tree = Trees.Single(t => t.x == x && t.y == y);

                IEnumerable<Tree> TreesAboveTree = Trees
                    .Where(t => t.x == x && t.y < y)
                    .OrderByDescending(t => t.y);

                var taCount = 0;
                foreach (var t in TreesAboveTree)
                {
                    taCount++;
                    if (t.Hight >= tree.Hight)
                    {
                        break;
                    }
                }

                IEnumerable<Tree> TreesBelowTree = Trees
                    .Where(t => t.x == x && t.y > y)
                    .OrderBy(t => t.y);

                var tbCount = 0;
                foreach (var t in TreesBelowTree)
                {
                    tbCount++;
                    if (t.Hight >= tree.Hight)
                    {
                        break;
                    }
                }

                IEnumerable<Tree> TreesRightOfTree = Trees
                    .Where(t => t.x > x && t.y == y)
                    .OrderBy(t => t.x);

                var trCount = 0;
                foreach (var t in TreesRightOfTree)
                {
                    trCount++;
                    if (t.Hight >= tree.Hight)
                    {
                        break;
                    }
                }

                IEnumerable<Tree> TreesLeftOfTree = Trees
                    .Where(t => t.x < x && t.y == y)
                    .OrderByDescending(t => t.x);

                var tlCount = 0;
                foreach (var t in TreesLeftOfTree)
                {
                    tlCount++;
                    if (t.Hight >= tree.Hight)
                    {
                        break;
                    }
                }


                var scenicScore = taCount * tbCount * trCount * tlCount;
                return scenicScore;
            }

        }
        public record Tree(int x, int y, int Hight);
    }
}