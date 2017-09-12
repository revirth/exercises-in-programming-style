using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _06_code_golf
{
    class Program
    {
        static void Main(string[] args)
        {
            var stops = File.ReadAllText("../stop_words.txt").Split(',').Union(Enumerable.Range('a', 26).Select(c => Convert.ToChar(c).ToString()));
		
		    var words = from x in Regex.Split(File.ReadAllText("../pride-and-prejudice.txt"), "[^a-zA-Z]+")
			where !stops.Contains(x.ToLower()) && !string.IsNullOrWhiteSpace(x)
			select x.ToLower();
		
            var uniqu_words =  words.GroupBy(w => w).OrderByDescending(w => w.Count());
            
            Console.WriteLine(string.Join("\n", uniqu_words.Take(25).Select(x => $"{x.Key}  -  {words.Count(w => w == x.Key)}")));
        }
    }
}
