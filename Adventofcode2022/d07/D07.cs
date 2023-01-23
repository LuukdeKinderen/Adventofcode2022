using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace d07
{
    public class D07
    {
        private static ElfDir currentDir = null!;
        private static int currentIndex = 0;
        private static string[] lines = null!;



        public static void Start()
        {
            currentDir = new ElfDir("root", null!);
            currentDir.dirs.Add(new ElfDir("/", currentDir));
            ElfDir root = currentDir;

            lines = File.ReadAllLines("D07.txt");
            string? line = lines[0];
            while (!string.IsNullOrEmpty(line))
            {
                Console.WriteLine(line);
                if (cdRegex.IsMatch(line))
                {
                    var matches = cdRegex.Match(line).Groups;
                    Cd(matches["dirName"].Value);
                }
                if (lsRegex.IsMatch(line))
                {
                    Ls();
                }
                try
                {
                    line = lines[currentIndex];
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Build complete fileTree");
                    break;
                }
            }

            //var dirs = dirsThatAreLessThan100000(root);
            //foreach (var dir in dirs)
            //{
            //    Console.WriteLine($"{dir.name} {dir.Size}");
            //}
            //Console.WriteLine($"Sum: {dirs.Sum(d => d.Size)}");

            var spaceFree = 70000000 - root.Size;
            var spaceNeeded = 30000000 - spaceFree;
            Console.WriteLine($"Space free: {spaceFree}");
            Console.WriteLine($"Space needed: {spaceNeeded}");

            var SmallestDir = DirAndSubDirs(root).Where(d => d.Size > spaceNeeded).Min();

            Console.WriteLine($"Smalles directory that, if deleted, would free up enough space: {SmallestDir.name}, {SmallestDir.Size}");
        }

        private static IEnumerable<ElfDir> DirAndSubDirs(ElfDir root)
        {
            yield return root;

            foreach (var dir in root.dirs)
            {
                foreach (var subDir in DirAndSubDirs(dir))
                {
                    yield return subDir;
                }
            }
        }

        private static IEnumerable<ElfDir> dirsThatAreLessThan100000(ElfDir root)
        {
            foreach (var dir in root.dirs)
            {
                if (dir.Size <= 100000)
                {
                    yield return dir;
                }
                foreach (var subdir in dirsThatAreLessThan100000(dir))
                {
                    yield return subdir;
                }
            }
        }

        private readonly static Regex cdRegex = new Regex(@"^\$ cd (?<dirName>[\w/.]+)$");
        private readonly static Regex lsRegex = new Regex(@"^\$ ls$");


        private static void Cd(string dirName)
        {
            if (dirName == "..")
            {
                currentDir = currentDir.parrent;
            }
            else
            {
                currentDir = currentDir.dirs.Single(d => d.name == dirName);
            }
            currentIndex++;
        }


        static readonly private Regex dirRegex = new Regex(@"^dir (?<dirName>[\w]+)$");
        static readonly private Regex fileRegex = new Regex(@"^(?<size>[\d]+) (?<name>[a-zA-Z.]+)$");


        private static void Ls()
        {

            string line = lines[++currentIndex];
            while (!line.StartsWith('$'))
            {

                if (dirRegex.IsMatch(line))
                {
                    var matches = dirRegex.Match(line).Groups;
                    string dirName = matches["dirName"].Value;
                    currentDir.dirs.Add(new ElfDir(dirName, currentDir));
                    Console.WriteLine($"Created dir: {dirName}");
                }
                else if (fileRegex.IsMatch(line))
                {
                    var matches = fileRegex.Match(line).Groups;
                    currentDir.files.Add(new ElfFile(int.Parse(matches["size"].Value), matches["name"].Value));
                    Console.WriteLine($"Created File: {matches["name"].Value}");
                }

                if (currentIndex + 1 == lines.Count())
                {
                    currentIndex++;
                    break;
                }

                line = lines[++currentIndex];
            }
        }
    }

    class ElfDir : IComparable<ElfDir>
    {
        public string name;
        public ElfDir parrent;

        public ElfDir(string name, ElfDir parrent)
        {
            this.name = name;
            this.parrent = parrent;
        }

        public List<ElfDir> dirs = new List<d07.ElfDir>();
        public List<ElfFile> files = new List<ElfFile>();

        public int Size => files.Sum(f => f.size) + dirs.Sum(d => d.Size);

        public int CompareTo(ElfDir? other)
        {
            if (other.Size > this.Size)
                return -1;
            else if (other.Size == this.Size)
                return 0;
            else
                return 1;
        }
    }

    record ElfFile(int size, string name);
}