using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01
{
    public class Elf
    {
        public List<int> snacks = new List<int>();

        public int TotalCalories => snacks.Sum();

        public void AddSnack(string snack)
        {
            snacks.Add(int.Parse(snack));
        }
    }
}
